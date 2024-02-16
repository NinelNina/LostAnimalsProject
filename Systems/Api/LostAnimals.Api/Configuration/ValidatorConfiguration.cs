namespace LostAnimals.Api.Configuration;

using LostAnimals.Common.Helpers;
using LostAnimals.Common.Validator;
using FluentValidation.AspNetCore;

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
