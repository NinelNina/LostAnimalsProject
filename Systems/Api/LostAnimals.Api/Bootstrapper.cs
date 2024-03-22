namespace LostAnimals.Api;

using LostAnimals.Services.Notes;
using LostAnimals.Services.AnimalKinds;
using LostAnimals.Services.Logger;
using LostAnimals.Services.Settings;
using LostAnimals.Services.Breeds;
using LostAnimals.Services.NoteCategories;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection service, IConfiguration configuration = null)
    {
        service
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddAppLogger()
            .AddNoteService()
            .AddAnimalKindService()
            .AddBreedService()
            .AddNoteCategoryService();

        return service;
    }
}
