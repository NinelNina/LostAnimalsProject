using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.NoteCategories;

public class UpdateNoteCategoryModel
{
    public string CategoryName { get; set; }
}

public class UpdateNoteCategoryModelProfile : Profile
{
    public UpdateNoteCategoryModelProfile()
    {
        CreateMap<UpdateNoteCategoryModel, NoteCategory>();
    }
}
