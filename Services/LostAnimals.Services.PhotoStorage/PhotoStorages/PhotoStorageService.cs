using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.PhotoStorages;

public class PhotoStorageService : IPhotoStorageService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public PhotoStorageService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<PhotoStorageModel> Create(CreatePhotoStorageModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photoStorage = mapper.Map<PhotoStorage>(model);

        await context.PhotoStorage.AddAsync(photoStorage);

        await context.SaveChangesAsync();

        return mapper.Map<PhotoStorageModel>(photoStorage);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photoStorage = await context.PhotoStorage.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (photoStorage == null)
        {
            throw new ProcessException($"PhotoStorage (ID = {id}) not found.");
        }

        context.PhotoStorage.Remove(photoStorage);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<PhotoStorageModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photoStorages = await context.PhotoStorage
                .ToListAsync();

        var result = mapper.Map<IEnumerable<PhotoStorageModel>>(photoStorages);

        return result;
    }

    public async Task<PhotoStorageModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var photoStorage = context.PhotoStorage
                .FirstOrDefault(x => x.Uid == id);

        var result = mapper.Map<PhotoStorageModel>(photoStorage);

        return result;
    }
}
