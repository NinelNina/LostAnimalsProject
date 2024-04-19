using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Breeds;

public class BreedService : IBreedService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public BreedService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<BreedModel> Create(CreateBreedModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var breed = mapper.Map<Breed>(model);

        await context.Breeds.AddAsync(breed);

        await context.SaveChangesAsync();

        return mapper.Map<BreedModel>(breed);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var breed = await context.Breeds.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (breed == null)
        {
            throw new ProcessException($"Breed (ID = {id}) not found.");
        }

        context.Breeds.Remove(breed);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<BreedModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var breeds = await context.Breeds
                .Include(x => x.AnimalKind)
                .ToListAsync();

        var result = mapper.Map<IEnumerable<BreedModel>>(breeds);

        return result;
    }

    public async Task<BreedModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var breed = context.Breeds
                .Include(x => x.AnimalKind)
                .FirstOrDefault(x => x.Uid == id);

        var result = mapper.Map<BreedModel>(breed);

        return result;
    }

    public async Task Update(Guid id, UpdateBreedModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var breed = await context.Breeds.Where(x => x.Uid == id).FirstOrDefaultAsync();

        breed = mapper.Map(model, breed);

        context.Breeds.Update(breed);

        await context.SaveChangesAsync();
    }
}
