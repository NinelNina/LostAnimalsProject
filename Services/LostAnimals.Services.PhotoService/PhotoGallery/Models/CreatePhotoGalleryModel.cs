using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using LostAnimals.Services.PhotoService.PhotoStorages;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.PhotoService.PhotoGalleries;

public class CreatePhotoGalleryModel
{
    public Guid Id { get; set; }
}

public class CreatePhotoGalleryModelProfile : Profile
{
    public CreatePhotoGalleryModelProfile()
    {
        CreateMap<CreatePhotoGalleryModel, PhotoGallery>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .AfterMap<CreatePhotoGalleryModelActions>();
    }
}

public class CreatePhotoGalleryModelActions : IMappingAction<CreatePhotoGalleryModel, PhotoGallery>
{
    public void Process(CreatePhotoGalleryModel source, PhotoGallery destination, ResolutionContext context)
    {
        destination.Uid = source.Id;
    }
}