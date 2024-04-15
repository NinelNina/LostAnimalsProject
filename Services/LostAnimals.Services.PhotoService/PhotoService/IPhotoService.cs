using LostAnimals.Context.Entities;
using LostAnimals.Services.PhotoService.PhotoGalleries;
using LostAnimals.Services.PhotoService.PhotoStorages;
using Microsoft.AspNetCore.Http;

namespace LostAnimals.Services.PhotoService;

public interface IPhotoService
{
    /// <summary>
    /// Create folder for photogallery.
    /// Save photogallery to database
    /// </summary>
    /// <returns></returns>
    //Task<PhotoGalleryModel> CreatePhotoGallery();
    /// <summary>
    /// Save photo into directory, created in CreatePhotoGallery().
    /// Save photoName to database.
    /// </summary>
    /// <param name="galleryId"></param>
    /// <param name="file"></param>
    /// <returns></returns>
    //Task<PhotoStorageModel> SavePhoto(Guid galleryId, IFormFile file);
    /// <summary>
    /// Get photos by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    //Task<PhotoGalleryModel> GetPhotoGalleryById(Guid id);

    //Task<Guid> UploadPhoto(IFormFile file);
    Task<Guid?> UploadPhoto(IFormFile file, Guid? photoGalleryId);

    Task<List<PhotoStorageModel>> GetPhotosByGalleryId(Guid galleryId);
}
