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

                    var galleryModel = new CreatePhotoGalleryModel { Id = galleryId };
                    var photoGallery = mapper.Map<PhotoGallery>(galleryModel);
                    await context.PhotoGallery.AddAsync(photoGallery);
                    await context.SaveChangesAsync();
                }
                else
                {
                    galleryId = photoGalleryId.Value;
                    path = Path.Combine(webHostEnvironment.WebRootPath, "images", galleryId.ToString());
                }

                var photoId = Guid.NewGuid();
                var photoName = path + "\\" + photoId + Path.GetExtension(file.FileName);
                path = Path.Combine(path, photoName);

                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                try
                {
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

    public async Task<List<PhotoStorageModel>> GetPhotosByGalleryId(Guid galleryId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var photoGallery = context.PhotoGallery
            .Include(x => x.PhotoStorages)
            .FirstOrDefault(x => x.Uid == galleryId);

        var photoStorages = photoGallery.PhotoStorages.ToList();

        Console.WriteLine("photoGallery.PhotoStorages: " + photoGallery.PhotoStorages.Count);
        Console.WriteLine("photoStorages.Count: " + photoStorages.Count);

        List<PhotoStorageModel> models = new List<PhotoStorageModel>();

        photoStorages.ForEach(x => models.Add(mapper.Map<PhotoStorageModel>(x)));

        //foreach (var photo in photoStorages)

        return models;
    }
}
