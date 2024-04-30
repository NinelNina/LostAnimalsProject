using LostAnimals.Web.Pages.Notes.Models;
using Microsoft.AspNetCore.Components.Forms;

namespace LostAnimals.Web.Pages.Notes.Services;

public interface INoteService
{
    Task<IEnumerable<NoteViewModel>> GetNotes();
    Task<NoteViewModel> GetNote(Guid noteId);
    Task<NoteViewModel> AddNote(CreateNoteViewModel model);
    Task EditNote(Guid noteId, UpdateNoteViewModel model);
    Task DeleteNote(Guid noteId);
    Task<bool> UploadPhotoAsync(Guid noteId, IBrowserFile file);
}
