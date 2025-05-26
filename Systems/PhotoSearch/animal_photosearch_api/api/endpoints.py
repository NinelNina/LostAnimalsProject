import json

from fastapi import APIRouter, UploadFile, File, Depends, HTTPException
from fastapi.responses import JSONResponse
from typing import Optional
import os
from PIL import Image
import io
from services.detection import PetDetector
from services.embedding import EmbeddingGenerator
from services.milvus import VectorDBService
from config import settings

router = APIRouter()


def get_detector():
    return PetDetector()


def get_embedder():
    return EmbeddingGenerator()


def get_vector_db():
    return VectorDBService()


@router.post("/detect")
async def detect_animals(
        file: UploadFile = File(...),
        detector: PetDetector = Depends(get_detector)
):
    try:
        image = await _read_image(file)
        detections = detector.detect(image)
        return {"detections": detections}
    except Exception as e:
        raise HTTPException(500, str(e))


@router.post("/insert")
async def insert_animal(
        file: UploadFile = File(...),
        animal_type: str = "unknown",
        metadata: str = "{}",
        embedder: EmbeddingGenerator = Depends(get_embedder),
        db: VectorDBService = Depends(get_vector_db)
):
    try:
        image = await _read_image(file)
        image_path = _save_uploaded_image(image)

        embedding = embedder.generate(image)
        item_id = db.insert(embedding, animal_type, image_path, json.loads(metadata))

        return {"status": "success", "id": item_id}
    except Exception as e:
        raise HTTPException(500, str(e))


async def _read_image(file: UploadFile) -> Image.Image:
    contents = await file.read()
    return Image.open(io.BytesIO(contents)).convert("RGB")


def _save_uploaded_image(image: Image.Image) -> str:
    os.makedirs(settings.upload_dir, exist_ok=True)
    image_path = f"{settings.upload_dir}/{len(os.listdir(settings.upload_dir))}.jpg"
    image.save(image_path)
    return image_path
