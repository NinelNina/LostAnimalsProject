using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.NoteCategories;

public class CreateNoteCategoryModel
{
    public string CategoryName { get; set; }
}

public class CreateNoteCategoryModelProfile : Profile
{
    public CreateNoteCategoryModelProfile()
    {
        CreateMap<CreateNoteCategoryModel, NoteCategory>();
    }
}