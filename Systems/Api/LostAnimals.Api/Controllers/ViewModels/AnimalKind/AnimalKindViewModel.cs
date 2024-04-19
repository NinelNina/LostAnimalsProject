using AutoMapper;
using LostAnimals.Api.Controllers.Models.Breed;
using LostAnimals.Services.AnimalKinds;

namespace LostAnimals.Api.Controllers.Models.AnimalKind;

public class AnimalKindViewModel
{
    public Guid Id { get; set; }

    public string AnimalKindName { get; set; }

    public ICollection<BreedViewModel>? Breeds { get; set; }
}

public class AnimalKindViewModelProfile : Profile
{
    public AnimalKindViewModelProfile()
    {
        CreateMap<AnimalKindModel, AnimalKindViewModel>();
    }
}