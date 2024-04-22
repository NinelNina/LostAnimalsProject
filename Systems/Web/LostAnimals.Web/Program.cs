using Blazored.LocalStorage;
using LostAnimals.Web;
using LostAnimals.Web.Pages.Notes.Services;
using LostAnimals.Web.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(Settings.ApiRoot) });

builder.Services.AddMudServices();
builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<INoteService, NoteService>();

await builder.Build().RunAsync();
