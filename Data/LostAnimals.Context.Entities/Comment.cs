namespace LostAnimals.Context.Entities
{
    public class Comment : BaseEntity
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int NoteID { get; set; }
        public virtual Note Note { get; set; }

        public int? ParentCommentID { get; set; }
        public virtual Comment? ParentComment { get; set; }

        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }

        public int? PhotoGalleryID { get; set; }
        public virtual PhotoGallery? PhotoGallery { get; set; }
    }
}
