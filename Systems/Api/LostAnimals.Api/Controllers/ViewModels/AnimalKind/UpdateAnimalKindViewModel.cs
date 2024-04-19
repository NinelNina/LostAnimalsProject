using AutoMapper;

namespace LostAnimals.Api.Controllers.Models.AnimalKind;

public class UpdateAnimalKindViewModel
{
    public string AnimalKindName { get; set; }
}

public class UpdateAnimalKindViewProfile : Profile
{
    public UpdateAnimalKindViewProfile()
    {
        CreateMap<UpdateAnimalKindViewModel, AnimalKindViewModel>();
    }
}