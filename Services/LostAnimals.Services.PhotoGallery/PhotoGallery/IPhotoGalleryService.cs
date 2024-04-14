namespace LostAnimals.Services.PhotoGalleries;

public interface IPhotoGalleryService
{
    /// <summary>
    /// Get all PhotoGalleries
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<PhotoGalleryModel>> GetAll();

    /// <summary>
    /// Get PhotoGallery by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<PhotoGalleryModel> GetById(Guid id);

    /// <summary>
    /// Create a new PhotoGallery
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    Task<PhotoGalleryModel> Create(Guid id);

    /// <summary>
    /// Delete PhotoGallery
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task Delete(Guid id);
}
