namespace LostAnimals.Identity;

public static class Bootstrapper
{
    public static IServiceCollection RegisterAppServices(this IServiceCollection services)
    {
        services
            .AddMainSettings()
            ;
        return services;
    }
}
