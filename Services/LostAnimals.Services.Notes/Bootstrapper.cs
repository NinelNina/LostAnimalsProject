using LostAnimals.Services.Notes;
using Microsoft.Extensions.DependencyInjection;

namespace LostAnimals.Services.Books;

public static class Bootstrapper
{
    public static IServiceCollection AddNoteService(this IServiceCollection services)
    {
        return services
            .AddSingleton<INoteService, NoteService>();
    }
}
