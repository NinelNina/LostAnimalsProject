﻿using IdentityServer4.AccessTokenValidation;
using LostAnimals.Common.Security;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using LostAnimals.Services.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;

namespace LostAnimals.Api.Configuration;

public static class AuthConfiguration
{
    public static IServiceCollection AddAppAuth(this IServiceCollection services, IdentitySettings settings)
    {
        IdentityModelEventSource.ShowPII = true;

        services
            .AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                opt.Password.RequiredLength = 0;
                opt.Password.RequireDigit = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireNonAlphanumeric = false;
            })
            .AddEntityFrameworkStores<MainDbContext>()
            .AddUserManager<UserManager<User>>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(options =>
        {
            options.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(IdentityServerAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.RequireHttpsMetadata = settings.Url.StartsWith("https://");
                options.Authority = settings.Url;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = false,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
                options.Audience = "api";
            });


        services.AddAuthorization(options =>
        {
            options.AddPolicy(AppScopes.AnimalKindsWrite, policy => policy.RequireClaim("scope", AppScopes.AnimalKindsWrite));
            options.AddPolicy(AppScopes.BreedsWrite, policy => policy.RequireClaim("scope", AppScopes.BreedsWrite));
            options.AddPolicy(AppScopes.NotesWrite, policy => policy.RequireClaim("scope", AppScopes.NotesWrite));
            options.AddPolicy(AppScopes.NoteCategoriesWrite, policy => policy.RequireClaim("scope", AppScopes.NoteCategoriesWrite));
            options.AddPolicy(AppScopes.CommentsWrite, policy => policy.RequireClaim("scope", AppScopes.CommentsWrite));
            options.AddPolicy(AppScopes.PhotosWrite, policy => policy.RequireClaim("scope", AppScopes.PhotosWrite));
            options.AddPolicy(AppScopes.UsersRead, policy => policy.RequireClaim("scope", AppScopes.UsersRead));
        });

        return services;
    }

    public static IApplicationBuilder UseAppAuth(this IApplicationBuilder app)
    {
        app.UseAuthentication();

        app.UseAuthorization();

        return app;
    }
}
