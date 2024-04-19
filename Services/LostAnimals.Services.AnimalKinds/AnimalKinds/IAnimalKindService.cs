namespace LostAnimals.Services.AnimalKinds;

public interface IAnimalKindService
{
    /// <summary>
    /// Get all animal kinds
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<AnimalKindModel>> GetAll();

    /// <summary>
    /// Get animal kind by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<AnimalKindModel> GetById(Guid id);

    /// <summary>
    /// Create a new animal kind
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<AnimalKindModel> Create(CreateAnimalKindModel model);

    /// <summary>
    /// Update animal kind
    /// </summary>
    /// <param name="id"></param>
    /// <param name="model"></param>
    /// <returns></returns>
    Task Update(Guid id, UpdateAnimalKindModel model);

    /// <summary>
    /// Delete animal kind
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(Guid id);
}
