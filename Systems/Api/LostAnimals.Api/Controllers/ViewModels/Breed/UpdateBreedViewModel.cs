using AutoMapper;
using LostAnimals.Services.Breeds;

namespace LostAnimals.Api.Controllers.Models.Breed;

public class UpdateBreedViewModel
{
    public string BreedName { get; set; }
}

public class UpdateBreedViewModelProfile : Profile
{
    public UpdateBreedViewModelProfile()
    {
        CreateMap<UpdateBreedModel, BreedViewModel>();
    }
}