namespace LostAnimals.Services.UserAccount;

public interface IUserAccountService
{
    /// <summary>
    /// Check if any user exists
    /// </summary>
    /// <returns></returns>
    Task<bool> IsEmpty();

    Task<IEnumerable<UserAccountModel>> GetAll();

    Task<UserAccountModel> GetById(Guid id);

    /// <summary>
    /// Create user account
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UserAccountModel> Create(RegisterUserAccountModel model);
}
