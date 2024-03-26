using FluentValidation;

namespace LostAnimals.Services.UserAccount;

public class RegisterUserAccountModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public class RegisterUserAccountModelValidator : AbstractValidator<RegisterUserAccountModel>
    {
        public RegisterUserAccountModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("User name is required.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Email is required.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password is too short.")
                .MaximumLength(50).WithMessage("Password is too long.");
        }
    }
}
