namespace LostAnimals.Api;

using LostAnimals.Api.Settings;
using LostAnimals.Context.Seeder;
using LostAnimals.Services.AnimalKinds;
using LostAnimals.Services.Breeds;
using LostAnimals.Services.Comments;
using LostAnimals.Services.EmailSender;
using LostAnimals.Services.Logger;
using LostAnimals.Services.NoteCategories;
using LostAnimals.Services.Notes;
using LostAnimals.Services.Settings;
using LostAnimals.Services.UserAccount;

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
            .AddDbSeeder()
            .AddEmailSenderSettings()
            .AddAppEmailSender()
            .AddAppLogger()
            .AddNoteService()
            .AddCommentService()
            .AddAnimalKindService()
            .AddBreedService()
            .AddNoteCategoryService()
            .AddUserAccountService()
            .AddPhotoService();

        return service;
    }
}
