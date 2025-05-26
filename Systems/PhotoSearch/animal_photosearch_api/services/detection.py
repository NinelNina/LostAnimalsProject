from ultralytics import YOLO
import cv2
import numpy as np
from PIL import Image
from typing import List, Dict
import os
from config import settings


class PetDetector:
    def __init__(self):
        self.model = YOLO(os.path.join(settings.model_dir, "best.pt"))

    def detect(self, image: Image.Image) -> List[Dict]:
        cv_image = cv2.cvtColor(np.array(image), cv2.COLOR_RGB2BGR)
        results = self.model(cv_image)

        detections = []
        for result in results:
            for box in result.boxes:
                if self._is_valid_detection(box):
                    detections.append(self._format_detection(box, image))

        return detections

    def _is_valid_detection(self, box) -> bool:
        cls = int(box.cls[0])
        conf = float(box.conf[0])
        return cls in [0, 1] and conf > 0.5

    def _format_detection(self, box, image) -> Dict:
        x1, y1, x2, y2 = map(int, box.xyxy[0].tolist())
        return {
            "type": "cat" if int(box.cls[0]) == 0 else "dog",
            "confidence": float(box.conf[0]),
            "bbox": [x1, y1, x2, y2],
            "cropped": image.crop((x1, y1, x2, y2))
        }