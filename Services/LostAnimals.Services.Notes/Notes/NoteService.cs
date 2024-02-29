using LostAnimals.Context;
using Microsoft.EntityFrameworkCore;


namespace LostAnimals.Services.Notes;

public class NoteService : INoteService
{ 
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public NoteService(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory; 
    }

    public async Task<NoteModel> Create(CreateNoteModel model)
    {
        /*using var context = await dbContextFactory.CreateDbContextAsync();

        var notes = await context.Notes.ToListAsync();

        var result = notes.Select(x => new NoteModel()
        {
            Id = x.Uid,
            UserId = x.User.Id,


        });*/
        throw new NotImplementedException();
    }

    public async Task Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<NoteModel>> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<NoteModel> GetById(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task Update(Guid id, UpdateNoteModel model)
    {
        throw new NotImplementedException();
    }
}

