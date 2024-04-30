using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LostAnimals.Services.UserAccount;
using LostAnimals.Context.Entities;
using Microsoft.AspNetCore.Identity;

namespace LostAnimals.Context.Seeder.Seeds;

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
        if (!(settings.Init?.AddDemoData ?? false))
            return;

        await using var context = DbContext(serviceProvider);

        DemoHelper demoHelper = new DemoHelper();

        if (!await context.AnimalKinds.AnyAsync())
        {
            await context.AddRangeAsync(demoHelper.GetAnimalKinds);
            await context.SaveChangesAsync();

            if (!await context.Breeds.AnyAsync())
            {
                await context.AddRangeAsync(demoHelper.ReadBreedsFromCsv(settings.Init?.BreedsCsv));
                await context.SaveChangesAsync();
            }
        }

        if (!await context.NoteCategories.AnyAsync())
        {
            await context.AddRangeAsync(demoHelper.GetNotesCategories);
            await context.SaveChangesAsync();
        }

        if (!await context.Users.AnyAsync())
        {
            var userManager = scope.ServiceProvider.GetService<UserManager<User>>();

            if (userManager != null)
            {
                await demoHelper.GetUsersAsync(userManager);
                await context.SaveChangesAsync();
            }
        }
        
        if (!await context.Notes.AnyAsync())
        {
            await context.AddRangeAsync(demoHelper.GetNotes.OrderBy(n => n.CreatedDate));
            await context.SaveChangesAsync();

            await context.AddRangeAsync(demoHelper.GetPhotoGallery());
            await context.SaveChangesAsync();

            var note = context.Notes.First();
            note.PhotoGalleryID = 1;

            context.Notes.Update(note);
            await context.SaveChangesAsync();

            if (!await context.Comments.AnyAsync())
            {
                await context.AddRangeAsync(demoHelper.GetComments.OrderBy(n => n.CreatedDate));
                await context.SaveChangesAsync();
            }
        }
    }

    private static async Task AddAdministrator(IServiceProvider serviceProvider)
    {
        using var scope = ServiceScope(serviceProvider);
        if (scope == null)
            return;

        var settings = scope.ServiceProvider.GetService<DbSettings>();
        if (!(settings.Init?.AddAdministrator ?? false))
            return;

        var userAccountService = scope.ServiceProvider.GetService<IUserAccountService>();

        if (!(await userAccountService.IsEmpty()))
            return;

        await userAccountService.Create(new RegisterUserAccountModel()
        {
            UserName = settings.Init.Administrator.Name,
            Email = settings.Init.Administrator.Email,
            Password = settings.Init.Administrator.Password,
        });
    }
}
