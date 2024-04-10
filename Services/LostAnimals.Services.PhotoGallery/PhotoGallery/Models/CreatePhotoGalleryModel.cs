using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.PhotoGalleries;

public class CreatePhotoGalleryModel
{
}

public class CreatePhotoGalleryModelProfile : Profile
{
    public CreatePhotoGalleryModelProfile()
    {
        CreateMap<CreatePhotoGalleryModel, PhotoStorage>();
    }
}