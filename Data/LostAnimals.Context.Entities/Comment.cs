namespace LostAnimals.Context.Entities
{
    public class Comment : BaseEntity
    {
        public int UserID { get; set; }
        public virtual User User { get; set; }

        public int NoteID { get; set; }
        public virtual Note Note { get; set; }

        public int AttributeID { get; set; }
        public virtual CommentAttribute Attribute { get; set; }

        public int? ParentCommentId { get; set; }
        public virtual Comment? ParentComment { get; set; }
        
        public string Content { get; set; }
        public virtual DateTime CreatedDate { get; set; }

        public int PhotoGalleryID { get; set; }
        public virtual PhotoGallery PhotoGallery { get; set; }
    }
}
