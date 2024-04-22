using AutoMapper;
using LostAnimals.Services.Breeds;

namespace LostAnimals.ViewModels.Breed;

public class BreedViewModel
{
    public Guid Id { get; set; }
    public string BreedName { get; set; }
}

public class BreedViewModelProfile : Profile
{
    public BreedViewModelProfile()
    {
        CreateMap<BreedModel, BreedViewModel>();
    }
}