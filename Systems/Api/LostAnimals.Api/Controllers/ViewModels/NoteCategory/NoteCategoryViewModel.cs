using AutoMapper;
using LostAnimals.Api.Controllers.Models.Note;
using LostAnimals.Services.NoteCategories;

namespace LostAnimals.Api.Controllers.Models.NoteCategory;

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
