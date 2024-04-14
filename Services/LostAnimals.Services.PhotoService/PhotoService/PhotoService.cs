using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using LostAnimals.Services.PhotoService.PhotoGalleries;
using LostAnimals.Services.PhotoService.PhotoStorages;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.PhotoService;

public class PhotoService : IPhotoService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IMapper mapper;

    public PhotoService(IDbContextFactory<MainDbContext> dbContextFactory, IWebHostEnvironment webHostEnvironment, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.webHostEnvironment = webHostEnvironment;
        this.mapper = mapper;
    }

    public async Task<Guid?> UploadPhoto(IFormFile file, Guid? photoGalleryId)
    {
        try
        {
            if (file.Length > 0)
            {
                var context = await dbContextFactory.CreateDbContextAsync();

                string? path;
                Guid galleryId;

                if (photoGalleryId == null)
                {
                    galleryId = Guid.NewGuid();
                    path = Path.Combine(webHostEnvironment.WebRootPath, "images", galleryId.ToString());
                    Directory.CreateDirectory(path);
                }
                else
                {
                    galleryId = photoGalleryId.Value;
                    path = Path.Combine(webHostEnvironment.WebRootPath, "images", galleryId.ToString());
                }

                var photoId = Guid.NewGuid();
                var photoName = photoId + Path.GetExtension(file.FileName);
                path = Path.Combine(path, photoName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                try
                {
                    var galleryModel = new CreatePhotoGalleryModel { Id = galleryId };
                    var photoGallery = mapper.Map<PhotoGallery>(galleryModel);
                    await context.PhotoGallery.AddAsync(photoGallery);
                    await context.SaveChangesAsync();

                    var photoStorageModel = new CreatePhotoStorageModel { Id = photoId, PhotoName = photoName, PhotoGalleryId = galleryId };
                    var photoStorage = mapper.Map<PhotoStorage>(photoStorageModel);
                    await context.PhotoStorage.AddAsync(photoStorage);
                    await context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    throw new ProcessException(ex);
                }
                return galleryId;
            }
            else
            {
                return null;
            }
        }
        catch
        {
            return null;
        }
    }

    public async Task<PhotoGalleryModel> GetPhotoGalleryById(Guid id)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var photoGallery = await context.PhotoGallery.FindAsync(id);

        return mapper.Map<PhotoGalleryModel>(photoGallery);
    }
}
