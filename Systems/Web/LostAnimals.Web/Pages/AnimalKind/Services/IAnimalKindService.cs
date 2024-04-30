using LostAnimals.Web.Pages.AnimalKind.Models;

namespace LostAnimals.Web.Pages.AnimalKind.Services;

public interface IAnimalKindService
{
    Task<IEnumerable<AnimalKindViewModel>> GetAnimalKinds();
}
