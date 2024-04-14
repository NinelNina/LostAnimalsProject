using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Notes;

public class UpdateNoteModel
{
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string? AnimalName { get; set; }

    public Guid BreedId { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryID { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public bool IsActive { get; set; } = true;

    public string PhoneNumber { get; set; }

    public DateTime LastSeenDate { get; set; }
    public DateTime LastEditDate { get; set;} = DateTime.Now;
}

public class UpdateNoteProfile : Profile
{
    public UpdateNoteProfile()
    {
        CreateMap<UpdateNoteModel, Note>()
            .ForMember(dest => dest.CategoryID, opt => opt.Ignore())
            .ForMember(dest => dest.BreedID, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoGalleryID, opt => opt.Ignore())
            .AfterMap<UpdateNoteModelActions>();
    }
}

public class UpdateNoteModelActions : IMappingAction<UpdateNoteModel, Note>
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public UpdateNoteModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(UpdateNoteModel source, Note destination, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var category = db.NoteCategories.FirstOrDefault(x => x.Uid == source.CategoryId);
        var breed = db.Breeds.FirstOrDefault(x => x.Uid == source.BreedId);
        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Uid == source.PhotoGalleryID);

        destination.CategoryID = category.Id;
        destination.BreedID = breed.Id;

        if (photoGallery != null)
        {
            destination.PhotoGalleryID = photoGallery.Id;
        }
    }
}