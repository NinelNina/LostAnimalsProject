using System;
using System.Collections.Generic;

namespace LostAnimals.Services.RabbitMqService
{
    public class PhotoProcessingMessage : IMessage
    {
        public Guid MessageId { get; } = Guid.NewGuid();
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        public string MessageType => "PhotoProcessing";

        public Guid NoteId { get; set; }
        public Guid PhotoId { get; set; }
        public string ImagePath { get; set; }
        public string AnimalType { get; set; }
        public Dictionary<string, object> Metadata { get; set; } = new();
    }
}
