import asyncio
import io
import os
import json
import aio_pika
import aiohttp
from typing import Optional, Dict, Any
from PIL import Image
import logging
from datetime import datetime
from services.embedding import EmbeddingGenerator
from services.milvus import VectorDBService

logger = logging.getLogger(__name__)


class RabbitMQConsumer:
    def __init__(
            self,
            embedder: EmbeddingGenerator,
            db: VectorDBService,
            queue_name: str = "animal_search_queue",
            rabbitmq_url: Optional[str] = None,
            dead_letter_queue: Optional[str] = None
    ):
        self.embedder = embedder
        self.db = db
        self.queue_name = queue_name
        self.rabbitmq_url = rabbitmq_url or os.getenv("RABBITMQ_URL", "amqp://guest:guest@rabbitmq/")
        self.dead_letter_queue = dead_letter_queue or f"{queue_name}_errors"
        self.connection = None
        self.channel = None
        self._should_reconnect = True
        self._reconnect_delay = 5
        self.api_base_url = os.getenv("LOST_ANIMALS_API_URL", "http://host.docker.internal:10000")

    async def connect(self):
        while self._should_reconnect:
            try:
                self.connection = await aio_pika.connect_robust(
                    self.rabbitmq_url,
                    client_properties={"connection_name": "python_consumer"}
                )
                self.channel = await self.connection.channel()
                await self.channel.set_qos(prefetch_count=1)
                await self.channel.declare_queue(self.dead_letter_queue, durable=True)
                logger.info("Successfully connected to RabbitMQ")
                return
            except Exception as e:
                logger.error(f"Connection failed: {e}. Retrying in {self._reconnect_delay} sec...")
                await asyncio.sleep(self._reconnect_delay)

    async def start_consuming(self):
        await self.connect()
        try:
            queue = await self.channel.declare_queue(self.queue_name, durable=True)
            logger.info(f"Started consuming queue: {self.queue_name}")
            async with queue.iterator() as queue_iter:
                async for message in queue_iter:
                    try:
                        await self.process_message(message)
                    except Exception as e:
                        logger.error(f"Unexpected error processing message: {e}")
                        await message.reject(requeue=False)
        except asyncio.CancelledError:
            logger.info("Consuming cancelled")
        finally:
            await self.close()

    async def process_message(self, message: aio_pika.IncomingMessage):
        async with message.process(requeue=True):
            try:
                start_time = datetime.now()
                data = await self._validate_message(message)
                logger.info(f"Processing photo {data['PhotoId']} from note {data['NoteId']}")
                embedding = await self._generate_embedding(f"{self.api_base_url}/{data['ImagePath'].lstrip('/')}")
                await self._save_to_milvus(data, embedding)
                processing_time = (datetime.now() - start_time).total_seconds()
                logger.info(f"Successfully processed photo {data['PhotoId']} in {processing_time:.2f} seconds")
            except (ValueError, FileNotFoundError, json.JSONDecodeError) as e:
                logger.error(f"Validation error: {e}")
                raise
            except Exception as e:
                logger.error(f"Processing failed: {e}")
                raise

    async def _validate_message(self, message: aio_pika.IncomingMessage) -> Dict[str, Any]:
        """Валидация входящего сообщения"""
        data = json.loads(message.body.decode())
        required_fields = {
            'ImagePath': str,
            'NoteId': str,
            'PhotoId': str,
            'AnimalType': str
        }
        missing_fields = [
            field for field, field_type in required_fields.items()
            if field not in data or not isinstance(data[field], field_type)
        ]
        if missing_fields:
            raise ValueError(f"Missing or invalid fields: {missing_fields}")
        image_url = f"{self.api_base_url}/{data['ImagePath'].lstrip('/')}"
        logger.info(f"Checking image availability at: {image_url}")
        async with aiohttp.ClientSession() as session:
            async with session.head(image_url) as response:
                if response.status != 200:
                    raise FileNotFoundError(f"Image not available: {image_url}")
        #data['ImageUrl'] = image_url
        return data

    async def _generate_embedding(self, image_url: str) -> list:
        """Генерация векторного эмбеддинга по URL"""
        try:
            async with aiohttp.ClientSession() as session:
                async with session.get(image_url) as response:
                    if response.status != 200:
                        raise FileNotFoundError(f"Failed to download image: {image_url}")
                    image_data = await response.read()
            image = Image.open(io.BytesIO(image_data)).convert("RGB")
            return self.embedder.generate(image)
        except Exception as e:
            logger.error(f"Embedding generation failed: {e}")
            raise

    async def _save_to_milvus(self, data: Dict[str, Any], embedding: list):
        try:
            await asyncio.wait_for(
                self.db.insert(
                    embedding=embedding,
                    animal_type=data['AnimalType'],
                    image_path=data['ImagePath'],
                    note_id=data['NoteId'],
                    photo_id=data['PhotoId'],
                    metadata=data.get('Metadata', {})
                ),
                timeout=30.0
            )
        except asyncio.TimeoutError:
            logger.error("Milvus insert timed out")
            raise
        except Exception as e:
            logger.error(f"Milvus insert failed: {e}")
            raise

    async def close(self):
        if self.connection:
            try:
                await self.connection.close()
                logger.info("RabbitMQ connection closed gracefully")
            except Exception as e:
                logger.error(f"Error closing connection: {e}")