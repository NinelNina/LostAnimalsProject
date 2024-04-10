using Microsoft.Extensions.DependencyInjection;

namespace LostAnimals.Services.PhotoGalleries;

public static class Bootstrapper
{
    public static IServiceCollection AddPhotoGalleryService(this IServiceCollection services)
    {
        return services
            .AddSingleton<IPhotoGalleryService, PhotoGalleryService>();
    }
}
