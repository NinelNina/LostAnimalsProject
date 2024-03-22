using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.NoteCategories;

public class NoteCategoryService : INoteCategoryService
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public NoteCategoryService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<NoteCategoryModel> Create(CreateNoteCategoryModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var noteCategory = mapper.Map<NoteCategory>(model);

        await context.NoteCategories.AddAsync(noteCategory);

        await context.SaveChangesAsync();

        return mapper.Map<NoteCategoryModel>(noteCategory);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var noteCategory = await context.NoteCategories.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (noteCategory == null)
        {
            throw new ProcessException($"NoteCategory (ID = {id}) not found.");
        }

        context.NoteCategories.Remove(noteCategory);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<NoteCategoryModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var noteCategorys = await context.NoteCategories
            //.Include(x => x.Notes)
            .ToListAsync();

        var result = mapper.Map<IEnumerable<NoteCategoryModel>>(noteCategorys);

        return result;
    }

    public async Task<NoteCategoryModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var noteCategory = context.NoteCategories
            //.Include(x => x.Notes)
            .FirstOrDefault(x => x.Uid == id);

        var result = mapper.Map<NoteCategoryModel>(noteCategory);

        return result;
    }

    public async Task Update(Guid id, UpdateNoteCategoryModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var noteCategory = await context.NoteCategories.Where(x => x.Uid == id).FirstOrDefaultAsync();

        noteCategory = mapper.Map(model, noteCategory);

        context.NoteCategories.Update(noteCategory);

        await context.SaveChangesAsync();
    }
}
