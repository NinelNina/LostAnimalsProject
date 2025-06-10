import asyncio

from pymilvus import connections, Collection, utility, FieldSchema, CollectionSchema, DataType
from typing import List, Dict, Optional
from config import settings


class VectorDBService:
    def __init__(self):
        self.collection_name = "animal_photos"
        self._connect()
        self._setup_collection()

    def _connect(self):
        connections.connect(
            alias="default",
            host=settings.milvus_host,
            port=int(settings.milvus_port),
            connect_timeout=10
        )

    def _setup_collection(self):
        if not utility.has_collection(self.collection_name):
            self._create_collection()

        self.collection = Collection(self.collection_name)
        self.collection.load()

    def _create_collection(self):
        fields = [
            FieldSchema(name="id", dtype=DataType.INT64, is_primary=True, auto_id=True),
            FieldSchema(name="embedding", dtype=DataType.FLOAT_VECTOR, dim=2048),
            FieldSchema(name="animal_type", dtype=DataType.VARCHAR, max_length=20),
            FieldSchema(name="image_path", dtype=DataType.VARCHAR, max_length=255),
            FieldSchema(name="note_id", dtype=DataType.VARCHAR, max_length=36),
            FieldSchema(name="photo_id", dtype=DataType.VARCHAR, max_length=36),
            FieldSchema(name="metadata", dtype=DataType.JSON)
        ]

        schema = CollectionSchema(fields, description="Animal search collection")
        self.collection = Collection(self.collection_name, schema)

        index_params = {
            "metric_type": "L2",
            "index_type": "IVF_FLAT",
            "params": {"nlist": 128}
        }

        self.collection.create_index(
            field_name="embedding",
            index_params=index_params
        )

    async def insert(
            self,
            embedding: List[float],
            animal_type: str,
            image_path: str,
            note_id: str,
            photo_id: str,
            metadata: Dict
    ) -> str:
        """Асинхронная версия метода вставки"""
        loop = asyncio.get_running_loop()
        return await loop.run_in_executor(
            None,
            self._blocking_insert,
            embedding,
            animal_type,
            image_path,
            note_id,
            photo_id,
            metadata
        )

    def _blocking_insert(
            self,
            embedding: List[float],
            animal_type: str,
            image_path: str,
            note_id: str,
            photo_id: str,
            metadata: Dict
    ) -> str:
        """Синхронная реализация вставки"""
        data = [
            [embedding],
            [animal_type],
            [image_path],
            [note_id],
            [photo_id],
            [metadata]
        ]

        mr = self.collection.insert(data)
        self.collection.flush()
        return mr.primary_keys[0]

    def search(
            self,
            query_embedding: List[float],
            animal_type: Optional[str] = None,
            limit: int = 5,
            output_fields: Optional[List[str]] = None
    ) -> List[Dict]:
        """Поиск с возможностью указания возвращаемых полей"""
        search_params = {
            "metric_type": "L2",
            "params": {"nprobe": 16}
        }

        expr = f'animal_type == "{animal_type}"' if animal_type else ""

        if output_fields is None:
            output_fields = ["animal_type", "image_path", "note_id", "photo_id"]

        results = self.collection.search(
            data=[query_embedding],
            anns_field="embedding",
            param=search_params,
            limit=limit,
            expr=expr,
            output_fields=output_fields
        )

        return [self._format_result(hit) for hits in results for hit in hits]

    def _format_result(self, hit) -> Dict:
        return {
            "id": str(hit.id),
            "score": float(hit.score),
            "animal_type": hit.entity.get("animal_type"),
            "image_path": hit.entity.get("image_path"),
            "note_id": hit.entity.get("note_id"),
            "photo_id": hit.entity.get("photo_id"),
            "metadata": hit.entity.get("metadata", {})
        }

    def get_by_photo_id(self, photo_id: str) -> Optional[Dict]:
        """Поиск по photo_id"""
        expr = f'photo_id == "{photo_id}"'
        results = self.collection.query(
            expr=expr,
            output_fields=["*"]
        )
        return results[0] if results else None

    def search_by_note_id(self, note_id: str) -> List[Dict]:
        """Поиск всех записей по note_id"""
        expr = f'note_id == "{note_id}"'
        return self.collection.query(
            expr=expr,
            output_fields=["*"]
        )
