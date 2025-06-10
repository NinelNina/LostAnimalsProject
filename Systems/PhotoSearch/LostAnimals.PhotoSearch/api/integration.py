# from fastapi import APIRouter, HTTPException, Depends
# from pydantic import BaseModel
# from typing import Optional
# from PIL import Image
# import os
#
# from config import settings
# from services.embedding import EmbeddingGenerator
# from services.milvus import VectorDBService
# from api.dependencies import get_vector_db, get_embedder
#
# router = APIRouter(tags=["Integration"])
#
#
# class SyncPhotoRequest(BaseModel):
#     note_id: str
#     photo_id: str
#     image_path: str
#     animal_type: Optional[str] = None
#     metadata: Optional[dict] = None
#
#
# @router.post("/sync_photo")
# async def sync_photo_with_search(
#         request: SyncPhotoRequest,
#         db: VectorDBService = Depends(get_vector_db),
#         embedder: EmbeddingGenerator = Depends(get_embedder)
# ):
#     try:
#         full_path = os.path.join(settings.STORAGE_ROOT, request.image_path.lstrip('/'))
#
#         if not os.path.exists(full_path):
#             raise FileNotFoundError(f"Image not found at {full_path}")
#
#         image = Image.open(full_path)
#         embedding = embedder.generate(image)
#
#         item_id = db.insert(
#             embedding=embedding,
#             animal_type=request.animal_type or "unknown",
#             image_path=request.image_path,
#             note_id=request.note_id,
#             photo_id=request.photo_id,
#             metadata=request.metadata or {}
#         )
#
#         return {"status": "success", "milvus_id": item_id}
#
#     except Exception as e:
#         raise HTTPException(500, detail=str(e))
#
#
# @router.get("/photos/by-note/{note_id}")
# async def get_photos_by_note_id(
#         note_id: str,
#         db: VectorDBService = Depends(get_vector_db)
# ):
#     try:
#         results = db.search_by_note_id(note_id)
#         return {"results": results}
#     except Exception as e:
#         raise HTTPException(500, detail=str(e))