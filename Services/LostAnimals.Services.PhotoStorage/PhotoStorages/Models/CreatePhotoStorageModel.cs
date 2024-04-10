using AutoMapper;
using LostAnimals.Context.Entities;
using LostAnimals.Context;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.PhotoStorages;

public class CreatePhotoStorageModel
{
    public Guid PhotoGalleryId { get; set; }
    public string PhotoFullName { get; set; }
}

public class CreatePhotoStorageModelProfile : Profile
{
    public CreatePhotoStorageModelProfile()
    {
        CreateMap<CreatePhotoStorageModel, PhotoStorage>()
            .ForMember(dest => dest.PhotoGalleryID, opt => opt.Ignore())
            .AfterMap<CreatePhotoStorageModelActions>();
    }
}

public class CreatePhotoStorageModelActions : IMappingAction<CreatePhotoStorageModel, PhotoStorage>
{
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public CreatePhotoStorageModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(CreatePhotoStorageModel source, PhotoStorage destination, ResolutionContext context)
    {
        using var db = contextFactory.CreateDbContext();

        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Uid == source.PhotoGalleryId);

        destination.PhotoGalleryID = photoGallery.Id;
    }
}