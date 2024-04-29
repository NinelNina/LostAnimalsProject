using LostAnimals.Web.Pages.Auth.Models;

namespace LostAnimals.Web.Pages.Auth.Services;

public interface IAuthService
{
    Task<LoginResult> Login(LoginModel loginModel);
    Task Logout();
    Task<bool> IsAccountConfirmed(LoginModel model);
    Task<UserAccountViewModel> Register(RegisterModel registerModel);
    Task<Result> ConfirmEmailAsync(string email, string token);
}
