using LostAnimals.Web.Pages.NoteCategory.Models;

namespace LostAnimals.Web.Pages.NoteCategory.Services;

public interface INoteCategoryService
{
    Task<IEnumerable<NoteCategoryViewModel>> GetCategories();
    Task<NoteCategoryViewModel> GetNoteCategory(Guid noteId);
}
