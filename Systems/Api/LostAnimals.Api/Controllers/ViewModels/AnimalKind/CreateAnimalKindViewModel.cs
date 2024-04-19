using AutoMapper;

namespace LostAnimals.Api.Controllers.Models.AnimalKind;

public class CreateAnimalKindViewModel
{
    public string AnimalKindName { get; set; }
}

public class CreateAnimalKindViewModelProfile : Profile
{
    public CreateAnimalKindViewModelProfile()
    {
        CreateMap<CreateAnimalKindViewModel, AnimalKindViewModel>();
    }
}