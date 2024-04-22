using AutoMapper;
using LostAnimals.Services.Breeds;

namespace LostAnimals.ViewModels.Breed;

public class CreateBreedViewModel
{
    public Guid AnimalKindID { get; set; }
    public string BreedName { get; set; }
}

public class CreateBreedViewModelProfile : Profile
{
    public CreateBreedViewModelProfile()
    {
        CreateMap<CreateBreedViewModel, CreateBreedModel>();
    }
}