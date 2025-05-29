namespace LostAnimals.Services.RabbitMqService;

using LostAnimals.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class Bootstrapper
{
    public static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration configuration = null)
    {
        var settings = Settings.Load<RabbitMqSettings>("RabbitMq", configuration);
        services.AddSingleton(settings);

        services.AddSingleton<IMessageQueueService, RabbitMqService>();

        return services;
    }
}