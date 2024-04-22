using AutoMapper;

namespace LostAnimals.ViewModels.AnimalKind;

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