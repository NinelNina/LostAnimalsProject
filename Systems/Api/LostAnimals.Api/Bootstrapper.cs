namespace LostAnimals.Api;

using LostAnimals.Services.Notes;
using LostAnimals.Services.AnimalKinds;
using LostAnimals.Services.Logger;
using LostAnimals.Services.Settings;
using LostAnimals.Services.Breeds;
using LostAnimals.Services.NoteCategories;
using LostAnimals.Services.UserAccount;
using LostAnimals.Api.Settings;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection service, IConfiguration configuration = null)
    {
        service
            .AddMainSettings()
            .AddApiSpecialSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddIdentitySettings()
            .AddAppLogger()
            .AddNoteService()
            .AddAnimalKindService()
            .AddBreedService()
            .AddNoteCategoryService()
            .AddUserAccountService();

        return service;
    }
}
