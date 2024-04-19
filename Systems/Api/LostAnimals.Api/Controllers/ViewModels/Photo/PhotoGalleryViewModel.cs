using AutoMapper;
using LostAnimals.Services.PhotoService.PhotoGalleries;

namespace LostAnimals.Api.Controllers.ViewModels.Photo;

public class PhotoGalleryViewModel
{
    public Guid Id { get; set; }
    public ICollection<PhotoStorageViewModel> photoStorages { get; set; }
}

public class PhotoGalleryViewModelProfile : Profile
{
    public PhotoGalleryViewModelProfile()
    {
        CreateMap<PhotoGalleryModel, PhotoGalleryViewModel>();
    }
}