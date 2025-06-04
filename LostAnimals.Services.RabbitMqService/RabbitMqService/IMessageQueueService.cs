namespace LostAnimals.Services.RabbitMqService;

using System.Threading.Tasks;

public delegate Task OnDataReceiveEvent<T>(T data);

public interface IMessageQueueService
{
    Task PushAsync<T>(string queueName, T data);
}