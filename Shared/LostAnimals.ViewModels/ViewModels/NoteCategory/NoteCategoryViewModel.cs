using AutoMapper;
using LostAnimals.ViewModels.Note;
using LostAnimals.Services.NoteCategories;

namespace LostAnimals.ViewModels.NoteCategory;

public class NoteCategoryViewModel
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; }
    public ICollection<NoteViewModel>? Notes { get; set; }
}

public class NoteCategoryViewModelProfile : Profile
{
    public NoteCategoryViewModelProfile()
    {
        CreateMap<NoteCategoryModel, NoteCategoryViewModel>();
    }
}
