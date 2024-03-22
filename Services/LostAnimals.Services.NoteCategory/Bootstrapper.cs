using Microsoft.Extensions.DependencyInjection;

namespace LostAnimals.Services.NoteCategories;

public static class Bootstrapper
{
    public static IServiceCollection AddNoteCategoryService(this IServiceCollection services)
    {
        return services
            .AddSingleton<INoteCategoryService, NoteCategoryService>();
    }
}
