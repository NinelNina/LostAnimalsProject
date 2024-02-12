namespace LostAnimals.Api.Configuration
{
    using Asp.Versioning.ApiExplorer;
    using LostAnimals.Services.Settings;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.Filters;
    using Swashbuckle.AspNetCore.SwaggerGen;
    using Swashbuckle.AspNetCore.SwaggerUI;
    using System.Reflection;

    public static class SwaggerConfiguration
    {
        private static string AppTitle = "Lost Animals API";

        public static IServiceCollection AddAppSwagger(this IServiceCollection services, MainSettings mainSettings, SwaggerSettings swaggerSettings)
        {
            if (!swaggerSettings.Enabled)
            {
                return services;
            }

            services
                .AddOptions<SwaggerGenOptions>()
                .Configure<IApiVersionDescriptionProvider>((options, provider) =>
                {
                    foreach (var apiVersionDescription in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerDoc(apiVersionDescription.GroupName,
                            new OpenApiInfo
                            {
                                Version = apiVersionDescription.GroupName,
                                Title = $"{AppTitle}"
                            });
                    }
                });

            services.AddSwaggerGen(options =>
            {
                options.SupportNonNullableReferenceTypes();

                options.UseInlineDefinitionsForEnums();

                options.ResolveConflictingActions(apiDescription => apiDescription.First());

                options.DescribeAllParametersInCamelCase();

                options.CustomSchemaIds(x => x.FullName);

                var xmlPath = Path.Combine(AppContext.BaseDirectory, "api.xml");
                if (File.Exists(xmlPath))
                {
                    options.IncludeXmlComments(xmlPath);
                }

                options.UseOneOfForPolymorphism();

                options.EnableAnnotations(true, true);

                options.UseAllOfForInheritance();

                options.UseOneOfForPolymorphism();

                options.SelectSubTypesUsing(baseType =>
                    typeof(Program).Assembly.GetTypes().Where(type => type.IsSubclassOf(baseType)));

                options.ExampleFilters();
            });

            services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

            services.AddSwaggerGenNewtonsoftSupport();

            return services;
        }

        public static void UseAppSwagger(this WebApplication app)
        {
            var mainSettings = app.Services.GetService<MainSettings>();
            var swaggerSettings = app.Services.GetService<SwaggerSettings>();

            if (!swaggerSettings?.Enabled ?? false)
                return;

            var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger(options => { options.RouteTemplate = "docs/{documentname}/api.yaml"; });

            app.UseSwaggerUI(
                options =>
                {
                    options.RoutePrefix = "docs";
                    provider.ApiVersionDescriptions.ToList().ForEach(
                        description =>
                            options.SwaggerEndpoint(
                                mainSettings.PublicUrl + $"/docs/{description.GroupName}/api.yaml",
                                description.GroupName.ToUpperInvariant())
                    );

                    options.DocExpansion(DocExpansion.List);
                    options.DefaultModelsExpandDepth(-1);
                    options.OAuthAppName(AppTitle);

                    options.OAuthClientId(swaggerSettings?.OAuthClientId ?? "");
                    options.OAuthClientSecret(swaggerSettings?.OAuthClientSecret ?? "");
                }
            );
        }
    }
}