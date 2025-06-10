import torch
import torch.nn as nn
import torchvision.models as models
from torchvision import transforms
from PIL import Image


class EmbeddingGenerator:
    def __init__(self):
        self.device = "cuda" if torch.cuda.is_available() else "cpu"
        self.model = models.resnet50(pretrained=True).to(self.device)
        self.model = nn.Sequential(*list(self.model.children())[:-1])
        self.model.eval()
        self.preprocess = transforms.Compose([
            transforms.Resize(256),
            transforms.CenterCrop(224),
            transforms.ToTensor(),
            transforms.Normalize(mean=[0.485, 0.456, 0.406],
                                 std=[0.229, 0.224, 0.225]),
        ])
        if 'cuda' in str(self.device):
            dummy_input = torch.randn(1, 3, 224, 224, device=self.device)
            self.model(dummy_input)
            torch.cuda.synchronize()

    def generate(self, image: Image.Image) -> list:
        image = self.preprocess(image).unsqueeze(0).to(self.device)
        with torch.no_grad():
            embedding = self.model(image)
            embedding = embedding.squeeze(-1).squeeze(-1)
        return embedding.cpu().numpy().flatten().tolist()
