using AutoMapper;
using LostAnimals.Context.Entities;
using LostAnimals.Context;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.PhotoService.PhotoStorages;

public class CreatePhotoStorageModel
{
    public Guid Id { get; set; }
    public Guid PhotoGalleryId { get; set; }
    public string PhotoName { get; set; }
}

public class CreatePhotoStorageModelProfile : Profile
{
    public CreatePhotoStorageModelProfile()
    {
        CreateMap<CreatePhotoStorageModel, PhotoStorage>()
        .ForMember(dest => dest.Id, opt => opt.Ignore())
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

        destination.Uid = source.Id;

        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Uid == source.PhotoGalleryId);
        destination.PhotoGalleryID = photoGallery.Id;
    }
}