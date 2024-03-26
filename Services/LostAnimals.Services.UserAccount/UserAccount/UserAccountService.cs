using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Common.Validator;
using LostAnimals.Context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.UserAccount.UserAccount;

public class UserAccountService : IUserAccountService
{
    private readonly IMapper mapper;
    private readonly UserManager<User> userManager;
    private readonly IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator;

    public UserAccountService(IMapper mapper, UserManager<User> userManager,
        IModelValidator<RegisterUserAccountModel> registerUserAccountModelValidator
    )
    {
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

        // Find user by email
        var user = await userManager.FindByEmailAsync(model.Email);
        if (user != null)
        {
            throw new ProcessException($"User account with email {model.Email} already exist.");
        }

        // Create user account
        user = new User()
        {
            UserName = model.UserName,
            Email = model.Email,
            EmailConfirmed = true, //TODO: подтверждение почты
            PhoneNumber = null,
            PhoneNumberConfirmed = false
            // ... Также здесь есть еще интересные свойства. Посмотрите в документации.
        };

        var result = await userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded)
        {
            throw new ProcessException($"Creating user account is wrong. {string.Join(", ", result.Errors.Select(s => s.Description))}");
        }

        return mapper.Map<UserAccountModel>(user);
    }
}
