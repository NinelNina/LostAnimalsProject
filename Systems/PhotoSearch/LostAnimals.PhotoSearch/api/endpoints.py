import io
from typing import Optional, List, Dict

from fastapi import APIRouter, Depends, File, HTTPException, UploadFile
from PIL import Image

from api.dependencies import get_detector, get_embedder, get_vector_db
from services.detection import PetDetector
from services.embedding import EmbeddingGenerator
from services.milvus import VectorDBService

router = APIRouter(tags=["Animal Search"])


async def _read_image(file: UploadFile) -> Image.Image:
    """Вспомогательная функция для чтения изображения"""
    contents = await file.read()
    return Image.open(io.BytesIO(contents)).convert("RGB")


@router.post("/detect")
async def detect_animals(
        file: UploadFile = File(...),
        detector: PetDetector = Depends(get_detector)
) -> Dict[str, List[Dict]]:
    try:
        image = await _read_image(file)
        detections = detector.detect(image)
        return {"detections": detections}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@router.post("/search")
async def search_by_photo(
        file: UploadFile = File(...),
        embedder: EmbeddingGenerator = Depends(get_embedder),
        db: VectorDBService = Depends(get_vector_db)
) -> Dict[str, List[Dict]]:
    try:
        image = await _read_image(file)
        embedding = embedder.generate(image)

        results = db.search(
            query_embedding=embedding,
            limit=5,
            output_fields=["note_id", "photo_id", "image_path"]
        )

        return {"results": results}
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))


@router.get("/photos/{photo_id}")
async def get_photo_details(
        photo_id: str,
        db: VectorDBService = Depends(get_vector_db)
) -> Dict:
    try:
        photo = db.get_by_photo_id(photo_id)
        if not photo:
            raise HTTPException(status_code=404, detail="Photo not found")
        return photo
    except Exception as e:
        raise HTTPException(status_code=500, detail=str(e))
