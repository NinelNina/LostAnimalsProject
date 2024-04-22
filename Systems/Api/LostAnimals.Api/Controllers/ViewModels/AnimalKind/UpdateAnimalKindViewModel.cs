using AutoMapper;

namespace LostAnimals.ViewModels.AnimalKind;

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