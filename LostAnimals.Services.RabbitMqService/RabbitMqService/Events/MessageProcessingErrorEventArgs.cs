namespace LostAnimals.Services.RabbitMqService;

public class MessageProcessingErrorEventArgs : EventArgs
{
    public Exception Exception { get; set; }
    public IMessage Message { get; set; }
    public byte[] RawMessage { get; set; }
    public string Operation { get; set; } // "Publishing" или "Processing"
}
