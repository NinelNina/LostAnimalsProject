using Microsoft.Extensions.DependencyInjection;

namespace LostAnimals.Services.Breeds;

public static class Bootstrapper
{
    public static IServiceCollection AddBreedService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IBreedService, BreedService>();
    }
}
