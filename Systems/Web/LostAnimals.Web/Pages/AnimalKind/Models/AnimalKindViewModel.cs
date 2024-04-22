using LostAnimals.Web.Pages.Breed.Models;

namespace LostAnimals.Web.Pages.AnimalKind.Models;

public class AnimalKindViewModel
{
    public Guid Id { get; set; }

    public string AnimalKindName { get; set; }

    public ICollection<BreedViewModel>? Breeds { get; set; }
}