using AutoMapper;
using LostAnimals.ViewModels.Breed;
using LostAnimals.Services.AnimalKinds;

namespace LostAnimals.ViewModels.AnimalKind;

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