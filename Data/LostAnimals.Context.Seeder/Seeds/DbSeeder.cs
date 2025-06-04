using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LostAnimals.Services.UserAccount;
using LostAnimals.Context.Entities;
using Microsoft.AspNetCore.Identity;
using LostAnimals.Context.Seeder;
using LostAnimals.Services.RabbitMqService;

namespace LostAnimals.Context.Seeder.Seeds
{
    public static class DbSeeder
    {
        private static IServiceScope ServiceScope(IServiceProvider serviceProvider)
        {
            return serviceProvider.GetService<IServiceScopeFactory>()!.CreateScope();
        }

        private static MainDbContext DbContext(IServiceProvider serviceProvider)
        {
            return ServiceScope(serviceProvider)
                .ServiceProvider.GetRequiredService<IDbContextFactory<MainDbContext>>().CreateDbContext();
        }

        public static void Execute(IServiceProvider serviceProvider)
        {
            Task.Run(async () =>
            {
                await AddDemoData(serviceProvider);
                await AddAdministrator(serviceProvider);
            })
                .GetAwaiter()
                .GetResult();
        }

        private static async Task AddDemoData(IServiceProvider serviceProvider)
        {
            using var scope = ServiceScope(serviceProvider);
            if (scope == null)
                return;

            var settings = scope.ServiceProvider.GetService<DbSettings>();
            if (!(settings?.Init?.AddDemoData ?? false))
                return;

            await using var context = DbContext(serviceProvider);
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
            var rabbitMqService = scope.ServiceProvider.GetRequiredService<IMessageQueueService>();
            var demoHelper = new DemoHelper();

            // Добавление видов животных
            if (!await context.AnimalKinds.AnyAsync())
            {
                await context.AddRangeAsync(demoHelper.GetAnimalKinds);
                await context.SaveChangesAsync();
            }

            // Добавление пород
            if (!await context.Breeds.AnyAsync())
            {
                await context.AddRangeAsync(demoHelper.GetBreeds());
                await context.SaveChangesAsync();
            }

            // Добавление категорий объявлений
            if (!await context.NoteCategories.AnyAsync())
            {
                await context.AddRangeAsync(demoHelper.GetNotesCategories);
                await context.SaveChangesAsync();
            }

            // Добавление пользователей
            if (!await context.Users.AnyAsync())
            {
                var users = await demoHelper.GetUsersAsync(userManager);
                await context.SaveChangesAsync();
            }

            // Добавление объявлений и комментариев
            if (!await context.Notes.AnyAsync())
            {
                var users = await context.Users.ToListAsync();
                var categories = await context.NoteCategories.ToListAsync();
                var breeds = await context.Breeds.ToListAsync();
                var notes = demoHelper.GetNotes(users, categories, breeds).ToList();

                await context.AddRangeAsync(notes);
                await context.SaveChangesAsync();

                // Отправка фотографий в RabbitMQ
                foreach (var note in notes)
                {
                    if (note.PhotoGallery?.PhotoStorages != null)
                    {
                        foreach (var photo in note.PhotoGallery.PhotoStorages)
                        {
                            var photoMessage = new PhotoProcessingMessage
                            {
                                NoteId = note.Uid,
                                PhotoId = photo.Uid,
                                ImagePath = photo.PhotoName,
                                AnimalType = note.Breed?.AnimalKind?.AnimalKindName ?? "Unknown"
                            };
                            await rabbitMqService.PushAsync("animal_photos", photoMessage);
                        }
                    }
                }

                var comments = demoHelper.GetComments(users, notes);
                await context.AddRangeAsync(comments);
                await context.SaveChangesAsync();
            }
        }

        private static async Task AddAdministrator(IServiceProvider serviceProvider)
        {
            using var scope = ServiceScope(serviceProvider);
            if (scope == null)
                return;

            var settings = scope.ServiceProvider.GetService<DbSettings>();
            if (!(settings?.Init?.AddAdministrator ?? false))
                return;

            var userAccountService = scope.ServiceProvider.GetService<IUserAccountService>();

            if (userAccountService == null || !(await userAccountService.IsEmpty()))
                return;

            await userAccountService.Create(new RegisterUserAccountModel()
            {
                UserName = settings.Init.Administrator.Name,
                Email = settings.Init.Administrator.Email,
                Password = settings.Init.Administrator.Password,
            });
        }
    }

    public class PhotoProcessingMessage
    {
        public Guid NoteId { get; set; }
        public Guid PhotoId { get; set; }
        public string ImagePath { get; set; }
        public string AnimalType { get; set; }
    }
}