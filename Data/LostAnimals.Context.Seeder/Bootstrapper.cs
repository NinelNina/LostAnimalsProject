using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LostAnimals.Context.Seeder;

public static class Bootstrapper
{
    public static IServiceCollection AddDbSeeder(this IServiceCollection services, IConfiguration configuration = null)
    {
        return services;
    }
}
