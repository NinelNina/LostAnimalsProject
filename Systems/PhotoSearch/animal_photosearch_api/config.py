from pydantic_settings import BaseSettings


class Settings(BaseSettings):
    milvus_host: str = "localhost"
    milvus_port: int = 19530
    model_dir: str = "models"
    upload_dir: str = "uploads"

    class Config:
        env_file = ".env"


settings = Settings()