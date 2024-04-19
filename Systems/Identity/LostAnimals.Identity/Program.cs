using LostAnimals.Context;
using LostAnimals.Identity;
using LostAnimals.Identity.Configuration;
using LostAnimals.Services.Settings;
using LostAnimals.Settings;
using Microsoft.AspNetCore.Identity;

var logSettings = Settings.Load<LogSettings>("Log");

var builder = WebApplication.CreateBuilder(args);

builder.AddAppLogger(logSettings);

// Configure services
var services = builder.Services;

services.Configure<DataProtectionTokenProviderOptions>(opt =>
   opt.TokenLifespan = TimeSpan.FromDays(1));

services.AddAppCors();

services.AddHttpContextAccessor();

services.AddAppDbContext(builder.Configuration);

services.AddAppHealthChecks();

services.RegisterAppServices();

services.AddIS4();


// Configure the HTTP request pipeline.

var app = builder.Build();

app.UseAppCors();

app.UseAppHealthChecks();

app.UseIS4();


// Run app

app.Run();