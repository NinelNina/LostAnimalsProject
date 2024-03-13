using Microsoft.Extensions.DependencyInjection;

namespace LostAnimals.Services.AnimalKinds;

public static class Bootstrapper
{
    public static IServiceCollection AddAnimalKindService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IAnimalKindService, AnimalKindService>();
    }
}
