using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context.Entities;
using LostAnimals.Context;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.PhotoGalleries;

public class PhotoGalleryService : IPhotoGalleryService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public PhotoGalleryService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<PhotoGalleryModel> Create(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
        
        CreatePhotoGalleryModel model = new CreatePhotoGalleryModel(id);

        var photoGallery = mapper.Map<Context.Entities.PhotoGallery>(model);
        
        //TODO: upload files

        await context.PhotoGallery.AddAsync(photoGallery);

        await context.SaveChangesAsync();

        return mapper.Map<PhotoGalleryModel>(photoGallery);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photoGallery = await context.PhotoGallery.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (photoGallery == null)
        {
            throw new ProcessException($"PhotoGallery (ID = {id}) not found.");
        }

        context.PhotoGallery.Remove(photoGallery);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PhotoGalleryModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photoGalleries = await context.PhotoGallery
                .Include(x => x.PhotoStorages)
                .ToListAsync();

        var result = mapper.Map<IEnumerable<PhotoGalleryModel>>(photoGalleries);

        return result;
    }

    public async Task<PhotoGalleryModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photoGallery = context.PhotoGallery
                .Include(x => x.PhotoStorages)
                .FirstOrDefault(x => x.Uid == id);

        var result = mapper.Map<PhotoGalleryModel>(photoGallery);

        return result;
    }
}
