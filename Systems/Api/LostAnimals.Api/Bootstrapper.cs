namespace LostAnimals.Api;

using LostAnimals.Services.Notes;
using LostAnimals.Services.AnimalKinds;
using LostAnimals.Services.Logger;
using LostAnimals.Services.Settings;
using LostAnimals.Services.Breeds;
using LostAnimals.Services.NoteCategories;
using LostAnimals.Services.UserAccount;
using LostAnimals.Api.Settings;
using LostAnimals.Services.Comments;
using LostAnimals.Services.EmailSender;

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
