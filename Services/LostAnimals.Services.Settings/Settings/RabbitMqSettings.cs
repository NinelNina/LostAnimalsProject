namespace LostAnimals.Settings;

public class RabbitMqSettings
{
    public string Url { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public int Port { get; set; }
    public string QueueName { get; set; }
    public string ExchangeName { get; set; }
}
