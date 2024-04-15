using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.PhotoService.PhotoStorages;


public class PhotoStorageModel
{
    public Guid Id { get; set; }
    public string PhotoName { get; set; }
}

public class PhotoStorageModelProfile : Profile
{
    public PhotoStorageModelProfile()
    {
        CreateMap<PhotoStorage, PhotoStorageModel>()
            .BeforeMap<PhotoStorageModelActions>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    public class PhotoStorageModelActions : IMappingAction<PhotoStorage, PhotoStorageModel>
    {
        private readonly IDbContextFactory<MainDbContext> dbContextFactory;

        public PhotoStorageModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public void Process(PhotoStorage source, PhotoStorageModel destination, ResolutionContext context)
        {
            using var db = dbContextFactory.CreateDbContext();

            var photoStorage = db.PhotoStorage
                .FirstOrDefault(x => x.Id == source.Id);

            destination.Id = photoStorage.Uid;
        }
    }
}
