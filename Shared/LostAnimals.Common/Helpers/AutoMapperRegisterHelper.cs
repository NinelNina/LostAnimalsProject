namespace LostAnimals.Common.Helpers;

using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class AutoMapperRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("lostanimals."));

        services.AddAutoMapper(assemblies);
    }
}