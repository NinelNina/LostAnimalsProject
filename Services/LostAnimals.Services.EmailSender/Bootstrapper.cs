using Microsoft.Extensions.DependencyInjection;

namespace LostAnimals.Services.EmailSender;

public static class Bootstrapper
{
    public static IServiceCollection AddAppEmailSender(this IServiceCollection services)
    {
        return services
                .AddScoped<IEmailSenderService, EmailSenderService>();
    }
}
