from services.detection import PetDetector
from services.embedding import EmbeddingGenerator
from services.milvus import VectorDBService


def get_detector() -> PetDetector:
    return PetDetector()


def get_embedder() -> EmbeddingGenerator:
    return EmbeddingGenerator()


def get_vector_db() -> VectorDBService:
    return VectorDBService()
