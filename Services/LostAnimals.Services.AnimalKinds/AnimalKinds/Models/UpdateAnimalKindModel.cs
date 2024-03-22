using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.AnimalKinds;

public class UpdateAnimalKindModel
{
    public string AnimalKindName { get; set; }
}

public class UpdateAnimalKindProfile : Profile
{
    public UpdateAnimalKindProfile()
    {
        CreateMap<UpdateAnimalKindModel, AnimalKind>();
    }
}
