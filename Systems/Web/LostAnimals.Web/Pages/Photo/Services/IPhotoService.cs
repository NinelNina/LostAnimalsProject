using LostAnimals.Web.Pages.Photo.Models;

namespace LostAnimals.Web.Pages.Photo.Services;

public interface IPhotoService
{
    Task<IEnumerable<PhotoStorageViewModel>> GetPhotosByGalleryId(Guid galleryId);
    Task DeletePhoto(Guid photoId);
}
