using LostAnimals.Web.Pages.Breed.Models;

namespace LostAnimals.Web.Pages.Breed.Services;

public interface IBreedService
{
    Task<IEnumerable<BreedViewModel>> GetBreeds();
}
