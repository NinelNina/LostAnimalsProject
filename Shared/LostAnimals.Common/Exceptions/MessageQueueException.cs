namespace LostAnimals.Common.Exceptions;

public class MessageQueueException : Exception
{
    public MessageQueueException(string message) : base(message) { }
    public MessageQueueException(string message, Exception innerException)
        : base(message, innerException) { }
}