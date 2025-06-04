import os

from fastapi import FastAPI
import asyncio
from api.endpoints import router as api_router
from api.integration import router as integration_router
from services.rabbitmq import RabbitMQConsumer
from services.embedding import EmbeddingGenerator
from services.milvus import VectorDBService
from config import settings
import logging

PHOTOSEARCH_API_URL = os.getenv("PHOTOSEARCH_API_URL", "http://localhost:8000")
LOST_ANIMALS_API_URL = os.getenv("LOST_ANIMALS_API_URL", "http://localhost:8080")


logging.basicConfig(level=settings.LOG_LEVEL)
logger = logging.getLogger(settings.APP_NAME)

app = FastAPI()
app.include_router(api_router, prefix="/api")
app.include_router(integration_router, prefix="/api")


@app.on_event("startup")
async def startup_event():
    embedder = EmbeddingGenerator()
    db = VectorDBService()

    consumer = RabbitMQConsumer(
        embedder=embedder,
        db=db,
        queue_name="animal_photos",
        rabbitmq_url=settings.RABBITMQ_URL
    )

    asyncio.create_task(consumer.start_consuming())
    logger.info("RabbitMQ consumer started")


@app.on_event("shutdown")
async def shutdown_event():
    logger.info("Application shutting down")


if __name__ == "__main__":
    import uvicorn

    uvicorn.run(
        app,
        host="0.0.0.0",
        port=8000,
        log_config=None
    )
