namespace LostAnimals.Context.Entities
{
    public class AnimalKind : BaseEntity
    {
        public string AnimalKindName { get; set; }

        public virtual ICollection<Breed> Breeds { get; set; }
    }
}