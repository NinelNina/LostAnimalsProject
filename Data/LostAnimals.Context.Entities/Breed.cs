namespace LostAnimals.Context.Entities
{
    public class Breed : BaseEntity
    {
        public string BreedName { get; set; }

        public virtual ICollection<Note>? Notes { get; set; }
        
        public int AnimalKindID { get; set; }
        public virtual AnimalKind AnimalKind { get; set; }
    }
}