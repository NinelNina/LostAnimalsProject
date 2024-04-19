﻿using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Common.Validator;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.UserAccount;

public class UserAccountService : IUserAccountService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;

    public UserAccountService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper, UserManager<User> userManager,
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator
    )
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
        this.userManager = userManager;
        this.registerUserAccountModelValidator = registerUserAccountModelValidator;
    }

    public async Task<bool> IsEmpty()
    {
        return !(await userManager.Users.AnyAsync());
    }

    public async Task<UserAccountModel> Create(RegisterUserAccountModel model)
    {
        registerUserAccountModelValidator.Check(model);

        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            throw new ProcessException($"User account with email {model.Email} already exist.");
        }

        user = new User()
        {
            UserName = model.UserName,
            Email = model.Email,
            EmailConfirmed = false,
            PhoneNumber = null,
            PhoneNumberConfirmed = false
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
        }

        return mapper.Map<UserAccountModel>(user);
    }

    public async Task<IEnumerable<UserAccountModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var users = await context.Users
                .ToListAsync();

        var result = mapper.Map<IEnumerable<UserAccountModel>>(users);

        return result;
    }

    public async Task<UserAccountModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var user = context.Users
                .FirstOrDefault(x => x.Id == id);

        var result = mapper.Map<UserAccountModel>(user);

        return result;
    }

    public async Task<string?> GenerateEmailConfirmationToken(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var user = context.Users
                .FirstOrDefault(x => x.Id == id);

        if (user == null)
        {
            return null;
        }

        if (!user.EmailConfirmed)
        {
            var token = await userManager.GenerateEmailConfirmationTokenAsync(user);

            return token;
        }
        else
        {
            return null;
        }
    }

    public async Task<UserAccountModel> FindByEmail(string email)
    {
        var user = await userManager.FindByEmailAsync(email);

        var result = mapper.Map<UserAccountModel>(user);

        return result;
    }

    public async Task<IdentityResult> ConfirmEmailAsync(string email, string token)
    {
        var user = await userManager.FindByEmailAsync(email);

        if (user == null)
        {
            return null;
        }

        if (!user.EmailConfirmed)
        {
            var result = await userManager.ConfirmEmailAsync(user, token);

            return result;
        }
        else
        {
            return null;
        }
    }
}
