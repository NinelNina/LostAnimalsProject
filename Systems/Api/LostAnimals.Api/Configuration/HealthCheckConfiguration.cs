namespace LostAnimals.Api.Configuration;

using LostAnimals.Api.HealthChecks;
using LostAnimals.Common;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using NetSchool.Common;

public static class HealthCheckConfiguration
{
    public static IServiceCollection AddAppHealthChecks(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<SelfHealthCheck>("LostAnimals.API");

        return services;
    }

    public static void UseAppHealthChecks(this WebApplication app)
    {
        app.MapHealthChecks("/health");

        app.MapHealthChecks("/health/detail", new HealthCheckOptions
        {
            ResponseWriter = HealthCheckHelper.WriteHealthCheckResponse,
            AllowCachingResponses = false
        });
    }
}
