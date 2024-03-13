using AutoMapper;
using LostAnimals.Common.Exceptions;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;


namespace LostAnimals.Services.Notes;

public class NoteService : INoteService
{ 
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;
    private readonly IMapper mapper;

    public NoteService(IDbContextFactory<MainDbContext> dbContextFactory, IMapper mapper)
    {
        this.dbContextFactory = dbContextFactory;
        this.mapper = mapper;
    }

    public async Task<NoteModel> Create(CreateNoteModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var note = mapper.Map<Note>(model);

        await context.Notes.AddAsync(note);

        await context.SaveChangesAsync();

        return mapper.Map<NoteModel>(note);
    }

    public async Task Delete(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var note = await context.Notes.Where(x => x.Uid == id).FirstOrDefaultAsync();

        if (note == null)
        {
            throw new ProcessException($"Note (ID = {id}) not found.");
        }

        context.Notes.Remove(note);

        await context.SaveChangesAsync();
    }

    public async Task<IEnumerable<NoteModel>> GetAll()
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var notes = await context.Notes
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.Comments)
                .Include(x => x.PhotoGallery)
                .ThenInclude(x => x.PhotoStorages)
                .ToListAsync();

        var result = mapper.Map<IEnumerable<NoteModel>>(notes);

        return result;
    }

    public async Task<NoteModel> GetById(Guid id)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();

        var note = context.Notes
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.Comments)
                .Include(x => x.PhotoGallery)
                .ThenInclude(x => x.PhotoStorages)
                .FirstOrDefault(x => x.Uid == id);

        var result = mapper.Map<NoteModel>(note);

        return result;
    }

    public async Task Update(Guid id, UpdateNoteModel model)
    {
        using var context = await dbContextFactory.CreateDbContextAsync();
       
        var note = await context.Notes.Where(x => x.Uid == id).FirstOrDefaultAsync();

        note = mapper.Map(model, note);

        context.Notes.Update(note);

        await context.SaveChangesAsync();
    }
}

