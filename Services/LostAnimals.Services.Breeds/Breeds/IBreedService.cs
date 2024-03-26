namespace LostAnimals.Services.Breeds;

public interface IBreedService
{
    /// <summary>
    /// Get all breeds
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<BreedModel>> GetAll();
    
    /// <summary>
    /// Get breed by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<BreedModel> GetById(Guid id);
    
    /// <summary>
    /// Create a new breed
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<BreedModel> Create(CreateBreedModel model);
    
    /// <summary>
    /// Update breed
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task Update(Guid id, UpdateBreedModel model);
    
    /// <summary>
    /// Delete breed
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(Guid id);
}
