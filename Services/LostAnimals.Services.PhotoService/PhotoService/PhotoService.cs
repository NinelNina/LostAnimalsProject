﻿using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using LostAnimals.Services.PhotoService.PhotoGalleries;
using LostAnimals.Services.PhotoService.PhotoStorages;
using LostAnimals.Services.Settings;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace LostAnimals.Services.PhotoService;

public class PhotoService : IPhotoService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IWebHostEnvironment webHostEnvironment;
    private readonly IMapper mapper;
    private readonly ILogger logger;

    public PhotoService(IDbContextFactory<MainDbContext> dbContextFactory, IWebHostEnvironment webHostEnvironment, IMapper mapper, ILogger logger)
    {
        this.dbContextFactory = dbContextFactory;
        this.webHostEnvironment = webHostEnvironment;
        this.mapper = mapper;
        this.logger = logger;
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

                    try
                    {
                        var galleryModel = new CreatePhotoGalleryModel { Id = galleryId };
                        var photoGallery = mapper.Map<PhotoGallery>(galleryModel);
                        await context.PhotoGallery.AddAsync(photoGallery);
                        await context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error saving photo gallery to database.");
                        throw new ProcessException(ex);
                    }
                }
                else
                {
                    galleryId = photoGalleryId.Value;
                    path = Path.Combine(webHostEnvironment.WebRootPath, "images", galleryId.ToString());
                }

                var photoId = Guid.NewGuid();
                var photoName = photoId + Path.GetExtension(file.FileName);
                path = Path.Combine(path, photoName);
                photoName = Path.Combine("images", galleryId.ToString(), photoName);

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
                    logger.Error(ex, "Error saving photo storage to database.");
                    throw new ProcessException(ex);
                }
                return galleryId;
            }
            else
            {
                throw new ProcessException("File length is null.");
            }
        }
        catch (Exception ex)
        {
            logger.Error(ex, "Error uploading photo.");
            throw new ProcessException(ex);
        }
    }


    public async Task<List<PhotoStorageModel>> GetPhotosByGalleryId(Guid galleryId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var photoGallery = context.PhotoGallery
            .Include(x => x.PhotoStorages)
            .FirstOrDefault(x => x.Uid == galleryId);

        if (photoGallery == null)
        {
            throw new ProcessException($"PhotoGallery (ID = {galleryId}) not found.");
        }

        var photoStorages = photoGallery.PhotoStorages.ToList();

        List<PhotoStorageModel> models = new List<PhotoStorageModel>();

        photoStorages.ForEach(x => models.Add(mapper.Map<PhotoStorageModel>(x)));

        return models;
    }

    public async Task DeletePhoto(Guid photoId)
    {
        var context = await dbContextFactory.CreateDbContextAsync();

        var photoStorage = await context.PhotoStorage
            .Include(x => x.PhotoGallery)
            .FirstOrDefaultAsync(x => x.Uid == photoId);

        if (photoStorage == null)
        {
            throw new ProcessException($"PhotoStorage (ID = {photoId}) not found.");
        }

        var galleryId = photoStorage.PhotoGallery.Uid;

        var imagePath = Path.Combine(webHostEnvironment.WebRootPath, "images", photoStorage.PhotoGallery.Uid.ToString(), photoStorage.PhotoName);

        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }

        context.PhotoStorage.Remove(photoStorage);
        await context.SaveChangesAsync();

        var photoGallery = await context.PhotoGallery
            .Include(x => x.PhotoStorages)
            .Where(x => x.Uid == galleryId)
            .FirstOrDefaultAsync();

        if (photoGallery == null)
        {
            throw new ProcessException($"PhotoGallery (ID = {galleryId}) not found.");
        }

        if (photoGallery.PhotoStorages.Count == 0)
        {
            context.PhotoGallery.Remove(photoGallery);

            await context.SaveChangesAsync();

            /*var path = Path.Combine(webHostEnvironment.WebRootPath, "images", galleryId.ToString());
            Directory.Delete(path);*/
        }
    }
}
