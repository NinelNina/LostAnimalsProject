using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using LostAnimals.Services.PhotoService.PhotoStorages;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.PhotoService.PhotoGalleries;

public class PhotoGalleryModel
{
    public Guid Id { get; set; }
    public ICollection<PhotoStorageModel> photoStorages { get; set; }
}

public class PhotoGalleryModelProfile : Profile
{
    public PhotoGalleryModelProfile()
    {
        CreateMap<PhotoGallery, PhotoGalleryModel>()
            .BeforeMap<PhotoGalleryModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    public class PhotoGalleryModelActions : IMappingAction<Context.Entities.PhotoGallery, PhotoGalleryModel>
    {
        private readonly IDbContextFactory<MainDbContext> dbContextFactory;

        public PhotoGalleryModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public void Process(Context.Entities.PhotoGallery source, PhotoGalleryModel destination, ResolutionContext context)
        {
            using var db = dbContextFactory.CreateDbContext();

            var photoGallery = db.PhotoGallery
                .FirstOrDefault(x => x.Id == source.Id);

            destination.Id = photoGallery.Uid;
        }
    }
}
