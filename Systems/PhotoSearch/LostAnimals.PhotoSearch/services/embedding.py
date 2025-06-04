import torch
from torchvision import transforms
from PIL import Image
from typing import List


class EmbeddingGenerator:
    def __init__(self):
        self.model = torch.hub.load('pytorch/vision:v0.10.0', 'resnet50', pretrained=True)
        self.model.eval()
        self.preprocess = transforms.Compose([
            transforms.Resize(256),
            transforms.CenterCrop(224),
            transforms.ToTensor(),
            transforms.Normalize(mean=[0.485, 0.456, 0.406], std=[0.229, 0.224, 0.225]),
        ])

    def generate(self, image: Image.Image) -> List[float]:
        img_tensor = self.preprocess(image)
        img_batch = img_tensor.unsqueeze(0)

        with torch.no_grad():
            embedding = self.model(img_batch).numpy()[0]

        return embedding.tolist()
