using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.Breeds;

public class UpdateBreedModel
{
    public string BreedName { get; set; }
    // public Guid AnimalKindID { get; set; }
}

public class UpdateBreedModelProfile : Profile
{
    public UpdateBreedModelProfile()
    {
        CreateMap<UpdateBreedModel, Breed>();
    }
}