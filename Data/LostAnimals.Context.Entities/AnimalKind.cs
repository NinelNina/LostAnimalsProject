namespace LostAnimals.Context.Entities
{
    public class AnimalKind : BaseEntity
    {
        public string AnimalKindName { get; set; }

        public ICollection<Breed> Breeds { get; set; }
    }
}