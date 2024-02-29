﻿namespace LostAnimals.Api;

using LostAnimals.Services.Books;
using LostAnimals.Services.Logger;
using LostAnimals.Services.Settings;

public static class Bootstrapper
{
    public static IServiceCollection RegisterServices(this IServiceCollection service, IConfiguration configuration = null)
    {
        service
            .AddMainSettings()
            .AddLogSettings()
            .AddSwaggerSettings()
            .AddAppLogger()
            .AddNoteService();

        return service;
    }
}
