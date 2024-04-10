using Microsoft.Extensions.DependencyInjection;

namespace LostAnimals.Services.PhotoStorages;

public static class Bootstrapper
{
    public static IServiceCollection AddPhotoStorageService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPhotoStorageService, PhotoStorageService>();
    }
}
