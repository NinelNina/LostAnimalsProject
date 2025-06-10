from fastapi import FastAPI
from fastapi.middleware.cors import CORSMiddleware
import asyncio
from api.endpoints import router as api_router
#from api.integration import router as integration_router
from services.rabbitmq import RabbitMQConsumer
from services.embedding import EmbeddingGenerator
from services.milvus import VectorDBService
from config import settings
import logging

PHOTOSEARCH_API_URL = settings.main_public_url
LOST_ANIMALS_API_URL = settings.lost_animals_api_url


logging.basicConfig(level=settings.log_level)
logger = logging.getLogger(settings.app_name)

app = FastAPI()
app.include_router(api_router, prefix="/api")
#app.include_router(integration_router, prefix="/api")
app.add_middleware(
    CORSMiddleware,
    allow_origins=["http://localhost:10002"],
    allow_credentials=True,
    allow_methods=["*"],
    allow_headers=["*"],
)


@app.on_event("startup")
async def startup_event():
    embedder = EmbeddingGenerator()
    db = VectorDBService()

    consumer = RabbitMQConsumer(
        embedder=embedder,
        db=db,
        queue_name="animal_photos",
        rabbitmq_url=settings.rabbitmq_url
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
