using LostAnimals.Context.Entities;
using LostAnimals.Services.PhotoService.PhotoGalleries;
using LostAnimals.Services.PhotoService.PhotoStorages;
using Microsoft.AspNetCore.Http;

namespace LostAnimals.Services.PhotoService;

public interface IPhotoService
{
    /// <summary>
    /// Upload photo to site
    /// Then upload photo path to database
    /// </summary>
    /// <param name="file"></param>
    /// <param name="photoGalleryId"></param>
    /// <returns></returns>
    Task<Guid?> UploadPhoto(IFormFile file, Guid? photoGalleryId);
    /// <summary>
    /// Get photos by galleryId
    /// </summary>
    /// <param name="galleryId"></param>
    /// <returns></returns>
    Task<List<PhotoStorageModel>> GetPhotosByGalleryId(Guid galleryId);
    /// <summary>
    /// Delete photo 
    /// </summary>
    /// <param name="photoId"></param>
    /// <returns></returns>
    Task DeletePhoto(Guid photoId);
}
