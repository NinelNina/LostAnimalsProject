using LostAnimals.Api;
using LostAnimals.Api.Configuration;
using LostAnimals.Services.Logger;
using LostAnimals.Services.Settings;
using LostAnimals.Settings;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings, logSettings);

var app = builder.Build();

var logger = app.Services.GetRequiredService<IAppLogger>();

logger.Information("LostAnimals.API has started");

app.Run();

logger.Information("LostAnimals.API has stopped");