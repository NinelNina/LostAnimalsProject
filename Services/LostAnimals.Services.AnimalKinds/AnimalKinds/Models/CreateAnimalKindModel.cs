using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.AnimalKinds;

public class CreateAnimalKindModel
{    public string AnimalKindName { get; set; }
}

public class AnimalKindCreateModelProfile : Profile
{
    public AnimalKindCreateModelProfile()
    {
        CreateMap<CreateAnimalKindModel, AnimalKind>();
    }
} 
