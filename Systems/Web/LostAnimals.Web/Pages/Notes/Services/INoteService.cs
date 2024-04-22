using LostAnimals.Web.Pages.Notes.Models;

namespace LostAnimals.Web.Pages.Notes.Services;

public interface INoteService
{
    Task<IEnumerable<NoteViewModel>> GetNotes();
    Task<NoteViewModel> GetNote(Guid noteId);
    Task AddNote(CreateNoteViewModel model);
    Task EditNote(Guid noteId, UpdateNoteViewModel model);
    Task DeleteNote(Guid noteId);
}
