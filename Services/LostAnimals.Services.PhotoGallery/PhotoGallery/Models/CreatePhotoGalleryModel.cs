﻿using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.PhotoGalleries;

public class CreatePhotoGalleryModel
{
    public CreatePhotoGalleryModel(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; set; }

}

public class CreatePhotoGalleryModelProfile : Profile
{
    public CreatePhotoGalleryModelProfile()
    {
        CreateMap<CreatePhotoGalleryModel, Context.Entities.PhotoGallery>();
    }
}