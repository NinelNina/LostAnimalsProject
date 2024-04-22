namespace LostAnimals.Web.Pages.Photo.Models;

public class PhotoGalleryViewModel
{
    public Guid Id { get; set; }
    public ICollection<PhotoStorageViewModel> photoStorages { get; set; }
}