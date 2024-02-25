namespace LostAnimals.Context.Entities
{
    public class Note : BaseEntity
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int CategoryID { get; set; }
        public virtual NoteCategory Category { get; set; }
        
        public string Title { get; set; }
        public string? AnimalName { get; set; }
        
        public int? BreedID { get; set; }
        public virtual Breed Breed { get; set; }

        public int? PhotoGalleryID { get; set; }
        public virtual PhotoGallery? PhotoGallery { get; set; }
        
        public virtual string Content { get; set;}
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
        public DateOnly? LastSeenDate { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Comment>? Comments { get; set; }
    }
}
