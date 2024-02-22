namespace LostAnimals.Context.Entities
{
    public class PhotoGallery : BaseEntity
    {
        public virtual ICollection<Note>? Notes {  get; set; }
        public virtual ICollection<Comment>? Comments {  get; set; }
        //public ICollection<Message>? Messages {  get; set; }
        public virtual ICollection<PhotoStorage>? PhotoStorages {  get; set; }
    }
}