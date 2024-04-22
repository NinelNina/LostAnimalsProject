using AutoMapper;
using LostAnimals.Services.UserAccount;

namespace LostAnimals.Web.Pages.UserAccount.Models;

public class UserAccountViewModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class UserAccountViewModelProfile : Profile
{
    public UserAccountViewModelProfile()
    {
        CreateMap<UserAccountModel, UserAccountViewModel>();
    }
}