using FluentValidation;
using LostAnimals.Services.UserAccount;

namespace LostAnimals.ViewModels.Account;

public class RegisterUserAccountViewModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public class RegisterUserAccountModelValidator : AbstractValidator<RegisterUserAccountViewModel>
    {
        public RegisterUserAccountModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("\"Имя пользователя\" - обязательное поле.");

            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("\"Email\" - обязательное поле.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Укажите пароль.")
                .MinimumLength(6).WithMessage("Пароль слишком короткий.")
                .MaximumLength(50).WithMessage("Пароль слишком длинный.");
        }
    }
}
