namespace LostAnimals.Services.UserAccount;

public interface IUserAccountService
{
    /// <summary>
    /// Check if any user exists
    /// </summary>
    /// <returns></returns>
    Task<bool> IsEmpty();

    /// <summary>
    /// Get all users
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<UserAccountModel>> GetAll();

    /// <summary>
    /// Get user by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<UserAccountModel> GetById(Guid id);

    /// <summary>
    /// Create user account
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<UserAccountModel> Create(RegisterUserAccountModel model);

    Task<string?> GenerateEmailConfirmationToken(Guid id);
}
