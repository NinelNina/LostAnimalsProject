using System;

namespace LostAnimals.Services.RabbitMqService
{
    public interface IMessage
    {
        Guid MessageId { get; }
        DateTime CreatedAt { get; }
        string MessageType { get; }
    }
}
