namespace LostAnimals.Services.Notes;

public interface INoteService
{
    Task<IEnumerable<NoteModel>> GetAll();
    Task<NoteModel> GetById(Guid id);
    Task<NoteModel> Create(CreateNoteModel model);
    Task Update(Guid id, UpdateNoteModel model);
    Task Delete(Guid id);
}
