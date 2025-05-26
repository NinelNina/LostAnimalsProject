from pymilvus import connections, Collection, utility, FieldSchema, CollectionSchema, DataType
from typing import List, Dict, Optional
import json
import os
from config import settings


class VectorDBService:
    def __init__(self):
        self.collection_name = "animals"
        self._connect()
        self._setup_collection()

    def _connect(self):
        connections.connect(
            alias="default",
            host=settings.milvus_host,
            port=settings.milvus_port
        )

    def _setup_collection(self):
        if not utility.has_collection(self.collection_name):
            self._create_collection()

        self.collection = Collection(self.collection_name)

    def _create_collection(self):
        fields = [
            FieldSchema(name="id", dtype=DataType.INT64, is_primary=True, auto_id=True),
            FieldSchema(name="embedding", dtype=DataType.FLOAT_VECTOR, dim=2048),
            FieldSchema(name="animal_type", dtype=DataType.VARCHAR, max_length=10),
            FieldSchema(name="image_path", dtype=DataType.VARCHAR, max_length=255),
            FieldSchema(name="metadata", dtype=DataType.JSON)
        ]

        schema = CollectionSchema(fields, description="Animal search collection")
        collection = Collection(self.collection_name, schema)

        index_params = {
            "metric_type": "L2",
            "index_type": "IVF_FLAT",
            "params": {"nlist": 128}
        }
        collection.create_index("embedding", index_params)

    def insert(self, embedding: List[float], animal_type: str, image_path: str, metadata: Dict) -> str:
        data = [
            [embedding],
            [animal_type],
            [image_path],
            [metadata]
        ]

        mr = self.collection.insert(data)
        self.collection.flush()
        return mr.primary_keys[0]

    def search(self, query_embedding: List[float], animal_type: Optional[str] = None, limit: int = 5) -> List[Dict]:
        search_params = {
            "metric_type": "L2",
            "params": {"nprobe": 16}
        }

        expr = f'animal_type == "{animal_type}"' if animal_type else ""

        results = self.collection.search(
            data=[query_embedding],
            anns_field="embedding",
            param=search_params,
            limit=limit,
            expr=expr,
            output_fields=["animal_type", "image_path", "metadata"]
        )

        return [self._format_result(hit) for hits in results for hit in hits]

    def _format_result(self, hit) -> Dict:
        return {
            "id": str(hit.id),
            "score": float(hit.score),
            "animal_type": hit.entity.get("animal_type"),
            "image_path": hit.entity.get("image_path"),
            "metadata": hit.entity.get("metadata")
        }