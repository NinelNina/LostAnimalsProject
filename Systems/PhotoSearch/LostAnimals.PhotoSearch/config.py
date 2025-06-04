from pydantic import Field
from pydantic_settings import BaseSettings


class Settings(BaseSettings):
    # RabbitMQ
    rabbitmq_url: str = Field(..., env="RABBITMQ_URL")

    # Storage
    storage_root: str = Field(..., env="STORAGE_ROOT")
    upload_dir: str = Field(..., env="UPLOAD_DIR")

    # Milvus
    milvus_host: str = Field(..., env="MILVUS_HOST")
    milvus_port: int = Field(..., env="MILVUS_PORT")
    milvus_collection: str = Field(..., env="MILVUS_COLLECTION")

    # Models
    model_dir: str = Field(..., env="MODEL_DIR")

    # Application
    app_name: str = Field(..., env="APP_NAME")
    log_level: str = Field(..., env="LOG_LEVEL")
    max_image_size: int = Field(..., env="MAX_IMAGE_SIZE")

    # API
    api_host: str = Field(..., env="API_HOST")
    api_port: int = Field(..., env="API_PORT")
    main_public_url: str = Field(..., env="MAIN_PUBLIC_URL")
    main_internal_url: str = Field(..., env="MAIN_INTERNAL_URL")
    lost_animals_api_url: str = Field(..., env="LOST_ANIMALS_API_URL")

    class Config:
        env_file = ".env"
        env_file_encoding = "utf-8"
        case_sensitive = False