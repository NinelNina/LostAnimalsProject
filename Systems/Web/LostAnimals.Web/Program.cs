using Blazored.LocalStorage;
using LostAnimals.Web;
using LostAnimals.Web.Pages.Auth.Services;
using LostAnimals.Web.Pages.Breed.Services;
using LostAnimals.Web.Pages.NoteCategory.Services;
using LostAnimals.Web.Pages.Notes.Services;
using LostAnimals.Web.Pages.Photo.Services;
using LostAnimals.Web.Providers;
using LostAnimals.Web.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Settings.ApiRoot) });

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<INoteService, NoteService>();
builder.Services.AddScoped<INoteCategoryService, NoteCategoryService>();
builder.Services.AddScoped<IPhotoService, PhotoService>();
builder.Services.AddScoped<IBreedService, BreedService>();

builder.Services.AddScoped<AuthenticationStateProvider, ApiAuthenticationStateProvider>();
builder.Services.AddScoped<IAuthService, AuthService>();


await builder.Build().RunAsync();
