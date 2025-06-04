using LostAnimals.Common.Exceptions;
using LostAnimals.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;
using System.Text.Json;
using System.Threading;

namespace LostAnimals.Services.RabbitMqService
{
    public class RabbitMqService : IMessageQueueService
    {
        private readonly RabbitMqSettings _settings;
        private readonly ILogger<RabbitMqService> _logger;
        private IConnection _connection;
        private readonly SemaphoreSlim _connectionLock = new(1, 1);
        private bool _isInitialized;

        public RabbitMqService(RabbitMqSettings settings, ILogger<RabbitMqService> logger)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (string.IsNullOrEmpty(_settings.Host))
                throw new ArgumentException("RabbitMQ Host cannot be null or empty.", nameof(_settings.Host));
            if (string.IsNullOrEmpty(_settings.Username))
                throw new ArgumentException("RabbitMQ UserName cannot be null or empty.", nameof(_settings.Username));
            if (string.IsNullOrEmpty(_settings.Password))
                throw new ArgumentException("RabbitMQ Password cannot be null or empty.", nameof(_settings.Password));
        }

        private async Task EnsureConnectionAsync()
        {
            if (_isInitialized && _connection?.IsOpen == true)
                return;

            await _connectionLock.WaitAsync();
            try
            {
                if (_isInitialized && _connection?.IsOpen == true)
                    return;

                const int maxRetries = 5;
                int attempt = 0;

                while (attempt < maxRetries)
                {
                    try
                    {
                        var factory = new ConnectionFactory
                        {
                            HostName = _settings.Host,
                            UserName = _settings.Username,
                            Password = _settings.Password,
                            AutomaticRecoveryEnabled = true
                        };

                        _connection = await factory.CreateConnectionAsync();
                        _isInitialized = true;
                        _logger.LogInformation("Successfully connected to RabbitMQ at {Host}", _settings.Host);
                        return;
                    }
                    catch (Exception ex)
                    {
                        attempt++;
                        _logger.LogWarning(ex, "Failed to connect to RabbitMQ (attempt {Attempt}/{MaxRetries})", attempt, maxRetries);
                        if (attempt == maxRetries)
                            throw new MessageQueueException("Failed to connect to RabbitMQ after maximum retries.", ex);

                        await Task.Delay(2000 * attempt);
                    }
                }
            }
            finally
            {
                _connectionLock.Release();
            }
        }

        private async Task<IChannel> CreateChannelAsync()
        {
            await EnsureConnectionAsync();
            if (_connection == null || !_connection.IsOpen)
                throw new MessageQueueException("RabbitMQ connection is not open.");

            try
            {
                var channel = await _connection.CreateChannelAsync();
                _logger.LogDebug("Created RabbitMQ channel.");
                return channel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to create RabbitMQ channel.");
                throw new MessageQueueException("Failed to create RabbitMQ channel.", ex);
            }
        }

        private async Task EnsureQueueExistsAsync(string queueName)
        {
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentNullException(nameof(queueName));

            try
            {
                var channel = await CreateChannelAsync();
                await using (channel.ConfigureAwait(false))
                {
                    await channel.QueueDeclareAsync(queue: queueName,
                                                    durable: true,
                                                    exclusive: false,
                                                    autoDelete: false,
                                                    arguments: null);
                    _logger.LogInformation("Declared queue {QueueName}", queueName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to declare queue {QueueName}", queueName);
                throw new MessageQueueException($"Failed to declare queue {queueName}.", ex);
            }
        }

        public async Task PushAsync<T>(string queueName, T data)
        {
            if (string.IsNullOrEmpty(queueName))
                throw new ArgumentNullException(nameof(queueName));
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            try
            {
                await EnsureQueueExistsAsync(queueName);

                var channel = await CreateChannelAsync();
                await using (channel.ConfigureAwait(false))
                {
                    var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(data));

                    var properties = new BasicProperties
                    {
                        Persistent = true,
                        Headers = new Dictionary<string, object>(),
                        MessageId = Guid.NewGuid().ToString(),
                        Timestamp = new AmqpTimestamp(DateTimeOffset.UtcNow.ToUnixTimeSeconds())
                    };

                    // Исправленный вызов BasicPublishAsync
                    await channel.BasicPublishAsync(
                        exchange: "",
                        routingKey: queueName,
                        mandatory: false, // Добавляем обязательный параметр
                        basicProperties: properties,
                        body: new ReadOnlyMemory<byte>(body)); // Конвертируем в ReadOnlyMemory

                    _logger.LogInformation("Published message to queue {QueueName}", queueName);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to publish message to queue {QueueName}", queueName);
                throw new MessageQueueException("Failed to publish message.", ex);
            }
        }
    }
}