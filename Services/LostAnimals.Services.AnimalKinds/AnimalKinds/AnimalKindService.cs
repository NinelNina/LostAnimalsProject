using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.AnimalKinds;

public class AnimalKindService : IAnimalKindService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public AnimalKindService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<AnimalKindModel> Create(CreateAnimalKindModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animalKind = mapper.Map<AnimalKind>(model);

        await context.AnimalKinds.AddAsync(animalKind);

        await context.SaveChangesAsync();

        return mapper.Map<AnimalKindModel>(animalKind);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animalKind = await context.AnimalKinds.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (animalKind == null)
        {
            throw new ProcessException($"AnimalKind (ID = {id}) not found.");
        }

        context.AnimalKinds.Remove(animalKind);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<AnimalKindModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animalKinds = await context.AnimalKinds
            .Include(x => x.Breeds)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<AnimalKindModel>>(animalKinds);

        return result;
    }

    public async Task<AnimalKindModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animalKind = context.AnimalKinds
            .Include(x => x.Breeds)
            .FirstOrDefault(x => x.Uid == id);

        var result = mapper.Map<AnimalKindModel>(animalKind);

        return result;
    }

    public async Task Update(Guid id, UpdateAnimalKindModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var animalKind = await context.AnimalKinds.Where(x => x.Uid == id).FirstOrDefaultAsync();

        animalKind = mapper.Map(model, animalKind);

        context.AnimalKinds.Update(animalKind);

        await context.SaveChangesAsync();
    }
}
