using AutoMapper;
using LostAnimals.Services.NoteCategories;

namespace LostAnimals.ViewModels.NoteCategory;

public class CreateNoteCategoryViewModel
{
    public string CategoryName { get; set; }
}

public class CreateNoteCategoryModelProfile : Profile
{
    public CreateNoteCategoryModelProfile()
    {
        CreateMap<CreateNoteCategoryViewModel, NoteCategoryModel>();
    }
}