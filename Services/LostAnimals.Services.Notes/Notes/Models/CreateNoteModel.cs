using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Notes;

public class CreateNoteModel
{
    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string? AnimalName { get; set; }

    public Guid BreedId { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }

    public string Region { get; set; }
    public string City { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public bool IsActive { get; set; } = true;

    public string PhoneNumber { get; set; }

    public DateTime LastSeenDate { get; set; }
    public DateTime CreatedDate { get; set; }
}


public class CreateNoteModelProfile : Profile
{
    public CreateNoteModelProfile()
    {
        CreateMap<CreateNoteModel, Note>()
            .ForMember(dest => dest.UserID, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryID, opt => opt.Ignore())
            .ForMember(dest => dest.BreedID, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoGalleryID, opt => opt.Ignore())
            .AfterMap<CreateNoteModelActions>();
    }
}

public class CreateNoteModelActions : IMappingAction<CreateNoteModel, Note>
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public CreateNoteModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(CreateNoteModel source, Note destination, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var user = db.Users.FirstOrDefault(x => x.Id == source.UserId);
        var category = db.NoteCategories.FirstOrDefault(x => x.Uid == source.CategoryId);
        var breed = db.Breeds.FirstOrDefault(x => x.Uid == source.BreedId);
        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Uid == source.PhotoGalleryId);

        destination.UserID = user.UserID;
        destination.CategoryID = category.Id;
        destination.BreedID = breed.Id;

        if (photoGallery != null)
        {
            destination.PhotoGalleryID = photoGallery.Id;
        }
    }
}