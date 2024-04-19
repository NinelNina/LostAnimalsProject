namespace LostAnimals.Api.Configuration;

using FluentValidation.AspNetCore;
using LostAnimals.Common.Helpers;
using LostAnimals.Common.Validator;

public static class ValidatorConfiguration
{
    public static IServiceCollection AddAppValidator(this IServiceCollection services)
    {
        services.AddFluentValidationAutoValidation(option => { option.DisableDataAnnotationsValidation = true; });

        FluentValidatorRegisterHelper.Register(services);

        services.AddSingleton(typeof(IModelValidator<>), typeof(ModelValidator<>));

        return services;
    }
}
