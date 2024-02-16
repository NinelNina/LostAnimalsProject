using LostAnimals.Api;
using LostAnimals.Api.Configuration;
using LostAnimals.Services.Logger;
using LostAnimals.Services.Settings;
using LostAnimals.Settings;

var mainSettings = Settings.Load<MainSettings>("Main");
var logSettings = Settings.Load<LogSettings>("Log");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(mainSettings, logSettings);

var services = builder.Services;

services.AddHttpContextAccessor();

services.AddAppCors();

services.AddAppHealthChecks();

services.AddAppVersioning();

services.AddAppSwagger(mainSettings, swaggerSettings);

services.AddAppAutoMappers();

services.AddAppValidator();

services.AddAppControllerAndViews();

services.RegisterServices(builder.Configuration);

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseAppSwagger();

app.UseAppControllerAndViews();

var logger = app.Services.GetRequiredService<IAppLogger>();

logger.Information("LostAnimals.API has started");

app.Run();

logger.Information("LostAnimals.API has stopped");