namespace LostAnimals.Services.PhotoStorages;

public interface IPhotoStorageService
{
    /// <summary>
    /// Get all PhotoStorages
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<PhotoStorageModel>> GetAll();

    /// <summary>
    /// Get PhotoStorage by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PhotoStorageModel> GetById(Guid id);

    /// <summary>
    /// Create a new PhotoStorage
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<PhotoStorageModel> Create(CreatePhotoStorageModel model);

    /// <summary>
    /// Delete PhotoStorage
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(Guid id);
}
