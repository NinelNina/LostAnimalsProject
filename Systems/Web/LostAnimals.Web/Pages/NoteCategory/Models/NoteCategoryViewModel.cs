using LostAnimals.Web.Pages.Notes.Models;

namespace LostAnimals.Web.Pages.NoteCategory.Models;

public class NoteCategoryViewModel
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public ICollection<NoteViewModel>? Notes { get; set; }
}
