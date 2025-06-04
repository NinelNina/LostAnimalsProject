import asyncio
import os
import json
import aio_pika
from typing import Optional, Dict, Any
from PIL import Image
from pymilvus import Collection
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
        """
        Улучшенный потребитель RabbitMQ

        :param embedder: Сервис генерации эмбеддингов
        :param db: Коллекция Milvus
        :param queue_name: Название основной очереди
        :param rabbitmq_url: URL подключения к RabbitMQ
        :param dead_letter_queue: Очередь для ошибочных сообщений
        """
        self.embedder = embedder
        self.db = db
        self.queue_name = queue_name
        self.rabbitmq_url = rabbitmq_url or os.getenv("RABBITMQ_URL", "amqp://guest:guest@rabbitmq/")
        self.dead_letter_queue = dead_letter_queue or f"{queue_name}_errors"
        self.connection = None
        self.channel = None
        self._should_reconnect = True
        self._reconnect_delay = 5

    async def connect(self):
        """Установка соединения с автоматическим переподключением"""
        while self._should_reconnect:
            try:
                self.connection = await aio_pika.connect_robust(
                    self.rabbitmq_url,
                    client_properties={"connection_name": "python_consumer"}
                )
                self.channel = await self.connection.channel()
                await self.channel.set_qos(prefetch_count=1)

                # Объявляем DLQ
                await self.channel.declare_queue(
                    self.dead_letter_queue,
                    durable=True
                )

                logger.info("Successfully connected to RabbitMQ")
                return
            except Exception as e:
                logger.error(f"Connection failed: {e}. Retrying in {self._reconnect_delay} sec...")
                await asyncio.sleep(self._reconnect_delay)

    async def start_consuming(self):
        """Основной цикл обработки сообщений"""
        await self.connect()

        try:
            queue = await self.channel.declare_queue(
                self.queue_name,
                durable=True,
                arguments={
                    "x-message-ttl": 86400000,
                    "x-max-length": 10000,
                    "x-dead-letter-exchange": "",
                    "x-dead-letter-routing-key": self.dead_letter_queue
                }
            )

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
        """Обработка сообщения с улучшенной валидацией"""
        async with message.process():
            try:
                start_time = datetime.now()
                data = self._validate_message(message)

                # Логирование начала обработки
                logger.info(
                    f"Processing photo {data['photo_id']} "
                    f"from note {data['note_id']}"
                )

                # Генерация эмбеддинга
                embedding = await self._generate_embedding(data['image_path'])

                # Сохранение в Milvus
                await self._save_to_milvus(data, embedding)

                # Логирование успешной обработки
                processing_time = (datetime.now() - start_time).total_seconds()
                logger.info(
                    f"Successfully processed photo {data['photo_id']} "
                    f"in {processing_time:.2f} seconds"
                )

            except (ValueError, FileNotFoundError) as e:
                logger.error(f"Validation error: {e}")
                await message.reject(requeue=False)
            except json.JSONDecodeError as e:
                logger.error(f"Invalid JSON: {e}")
                await message.reject(requeue=False)
            except Exception as e:
                logger.error(f"Processing failed: {e}")
                await message.reject(requeue=True)
                raise

    def _validate_message(self, message: aio_pika.IncomingMessage) -> Dict[str, Any]:
        """Валидация входящего сообщения"""
        data = json.loads(message.body.decode())

        required_fields = {
            'image_path': str,
            'note_id': str,
            'photo_id': str,
            'animal_type': str
        }

        missing_fields = [
            field for field, field_type in required_fields.items()
            if field not in data or not isinstance(data[field], field_type)
        ]

        if missing_fields:
            raise ValueError(f"Missing or invalid fields: {missing_fields}")

        # Проверка существования файла
        full_path = os.path.join(
            os.getenv("STORAGE_ROOT", "/storage"),
            data['image_path'].lstrip('/')
        )

        if not os.path.exists(full_path):
            raise FileNotFoundError(f"Image file not found: {full_path}")

        data['full_path'] = full_path
        return data

    async def _generate_embedding(self, image_path: str) -> list:
        """Генерация векторного эмбеддинга"""
        try:
            image = Image.open(image_path)
            return self.embedder.generate(image)
        except Exception as e:
            logger.error(f"Embedding generation failed: {e}")
            raise

    async def _save_to_milvus(self, data: Dict[str, Any], embedding: list):
        try:
            await asyncio.wait_for(
                self.db.insert(
                    embedding=embedding,
                    animal_type=data['animal_type'],
                    image_path=data['image_path'],
                    note_id=data['note_id'],
                    photo_id=data['photo_id'],
                    metadata=data.get('metadata', {})
                ),
                timeout=10.0
            )
        except asyncio.TimeoutError:
            logger.error("Milvus insert timed out")
            raise
        except Exception as e:
            logger.error(f"Milvus insert failed: {e}")
            raise

    async def close(self):
        """Закрытие соединения"""
        if self.connection:
            try:
                await self.connection.close()
                logger.info("RabbitMQ connection closed gracefully")
            except Exception as e:
                logger.error(f"Error closing connection: {e}")
