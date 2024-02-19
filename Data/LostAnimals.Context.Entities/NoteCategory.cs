namespace LostAnimals.Context.Entities
{
    public class NoteCategory : BaseEntity
    {
        public string CategoryName { get; set; }
        public ICollection<Note> Notes { get; set;}
    }
}