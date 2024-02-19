namespace LostAnimals.Context.Entities
{
    public class PhotoStorage : BaseEntity
    {
        public string PhotoFullName { get; set; }

        public int PhotoGalleryID { get; set; }
        public virtual PhotoGallery PhotoGallery { get; set; }
    }
}