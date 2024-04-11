using Microsoft.AspNetCore.Identity;

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
    
    /// <summary>
    /// Generate email confirmation token 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<string?> GenerateEmailConfirmationToken(Guid id);
    
    /// <summary>
    /// Find user by email
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<UserAccountModel> FindByEmail(string email);
    
    /// <summary>
    /// Confirm user account
    /// </summary>
    /// <param name="email"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<IdentityResult> ConfirmEmailAsync(string email, string token);
}
