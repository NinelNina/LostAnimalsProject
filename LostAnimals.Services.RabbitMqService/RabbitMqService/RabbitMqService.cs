using LostAnimals.Common.Exceptions;
using LostAnimals.Services.Settings;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;

namespace LostAnimals.Services.RabbitMqService;

public class RabbitMqService : IMessageQueueService
{
    private const int ConnectRetriesCount = 10;
    private readonly RabbitMqSettings settings;
    private readonly IConnection connection;
    private readonly IChannel channel;
    private bool disposed;

    public RabbitMqService(RabbitMqSettings settings)
    {
        settings = settings ?? throw new ArgumentNullException(nameof(settings));
        (connection, channel) = InitializeConnectionAsync().GetAwaiter().GetResult();
    }

    private readonly SemaphoreSlim connectionSemaphore = new SemaphoreSlim(1, 1);

    private async Task<(IConnection, IChannel)> InitializeConnectionAsync()
    {
        await connectionSemaphore.WaitAsync();
        try
        {
            var factory = new ConnectionFactory
            {
                HostName = settings.Host,
                Port = settings.Port,
                UserName = settings.Username,
                Password = settings.Password,
                AutomaticRecoveryEnabled = true,
                NetworkRecoveryInterval = TimeSpan.FromSeconds(5)
            };

            IConnection connection = null;
            IChannel channel = null;

            for (var i = 0; i < ConnectRetriesCount; i++)
            {
                try
                {
                    connection = await factory.CreateConnectionAsync();
                    channel = await connection.CreateChannelAsync();
                    await channel.BasicQosAsync(0, 1, false);
                    return (connection, channel);
                }
                catch (BrokerUnreachableException)
                {
                    await Task.Delay(500);
                    connection?.Dispose();
                    channel?.Dispose();
                }
            }

            throw new InvalidOperationException("Failed to connect to RabbitMQ after retries");
        }
        finally
        {
            connectionSemaphore.Release();
        }
    }

    public async Task PushAsync<T>(string queueName, T data)
    {
        if (disposed)
            throw new ObjectDisposedException(nameof(RabbitMqService));

        try
        {
            await EnsureQueueExistsAsync(queueName);

            var json = JsonSerializer.Serialize(data);
            var body = Encoding.UTF8.GetBytes(json);

            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: queueName,
                body: body);
        }
        catch (Exception ex)
        {
            throw new MessageQueueException("Failed to publish message", ex);
        }
    }

    public async Task Subscribe<T>(string queueName, OnDataReceiveEvent<T> onReceive)
    {
        if (disposed)
            throw new ObjectDisposedException(nameof(RabbitMqService));

        if (onReceive == null)
            return;

        await EnsureQueueExistsAsync(queueName);

        var consumer = new AsyncEventingBasicConsumer(channel);
        consumer.ReceivedAsync += async (_, eventArgs) =>
        {
            try
            {
                var message = Encoding.UTF8.GetString(eventArgs.Body.Span);
                var obj = JsonSerializer.Deserialize<T>(message);

                await onReceive(obj);
                await channel.BasicAckAsync(eventArgs.DeliveryTag, false);
            }
            catch (Exception ex)
            {
                await channel.BasicNackAsync(eventArgs.DeliveryTag, false, false);
                throw new MessageQueueException("Failed to process message", ex);
            }
        };

        await channel.BasicConsumeAsync(
            queue: queueName,
            autoAck: false,
            consumer: consumer);
    }

    private async Task EnsureQueueExistsAsync(string queueName)
    {
        await channel.QueueDeclareAsync(
            queue: queueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public void Dispose()
    {
        if (disposed) return;

        disposed = true;
        channel?.CloseAsync();
        connection?.CloseAsync();
        channel?.Dispose();
        connection?.Dispose();
    }
}