using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.PhotoService.PhotoGalleries;

public class CreatePhotoGalleryModel
{
    public Guid Id { get; set; }
}

public class CreatePhotoGalleryModelProfile : Profile
{
    public CreatePhotoGalleryModelProfile()
    {
        CreateMap<CreatePhotoGalleryModel, PhotoGallery>();
    }
}