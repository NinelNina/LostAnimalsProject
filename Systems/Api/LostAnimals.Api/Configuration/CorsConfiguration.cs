namespace LostAnimals.Api.Configuration;

using LostAnimals.Services.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

public static class CorsConfiguration
{
    public static IServiceCollection AddAppCors(this IServiceCollection services)
    {
        services.AddCors();

        return services;
    }

    public static void UseAppCors(this WebApplication app)
    {
        var mainSettings = app.Services.GetService<MainSettings>();

        var origins = mainSettings.AllowedOrigins.Split(',', ';').Select(x => x.Trim())
            .Where(x => !string.IsNullOrEmpty(x)).ToArray();

        app.UseCors(pol =>
        {
            pol.AllowAnyHeader();
            pol.AllowAnyMethod();
            pol.AllowCredentials();
            if (origins.Length > 0)
                pol.WithOrigins(origins);
            else
                pol.SetIsOriginAllowed(origin => true);

            pol.WithExposedHeaders("Content-Disposition");
        });
    }
}