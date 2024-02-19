namespace LostAnimals.Context.Entities
{
    public class PhotoGallery : BaseEntity
    {
        public ICollection<Note>? Notes {  get; set; }
        public ICollection<Comment>? Comments {  get; set; }
        public ICollection<Message>? Messages {  get; set; }
        public ICollection<PhotoStorage>? PhotoStorages {  get; set; }
    }
}