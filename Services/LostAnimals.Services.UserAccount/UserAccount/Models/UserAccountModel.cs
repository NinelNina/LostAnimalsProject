using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.UserAccount;

public class UserAccountModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
}

public class UserAccountModelProfile : Profile
{
    public UserAccountModelProfile()
    {
        CreateMap<User, UserAccountModel>();
    }
}