namespace LostAnimals.Common.Helpers;

using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

public static class FluentValidatorRegisterHelper
{
    public static void Register(IServiceCollection services)
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies()
            .Where(s => s.FullName != null && s.FullName.ToLower().StartsWith("lostanimals."));

        assemblies.ToList().ForEach(x => { services.AddValidatorsFromAssembly(x, ServiceLifetime.Singleton); });
    }
}
