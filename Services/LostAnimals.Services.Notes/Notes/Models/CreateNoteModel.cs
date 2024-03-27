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
    public string Description { get; set; }
    public string? AnimalName { get; set; }

    public Guid BreedId { get; set; }

    public string Text { get; set; }

    public Guid? PhotoGalleryID { get; set; }

    public string Region { get; set; }
    public string City { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public DateTime LastSeenDate { get; set; } //TODO: проблема маппинга DateTime -> DateOnly
                                               //System.DateTime -> System.DateOnly
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
    private readonly IDbContextFactory<MainDbContext> contextFactory;

    public CreateNoteModelActions(IDbContextFactory<MainDbContext> contextFactory)
    {
        this.contextFactory = contextFactory;
    }

    public void Process(CreateNoteModel source, Note destination, ResolutionContext context)
    {
        using var db = contextFactory.CreateDbContext();

        var user = db.Users.FirstOrDefault(x => x.Id == source.UserId);
        var category = db.NoteCategories.FirstOrDefault(x => x.Uid == source.CategoryId);
        var breed = db.Breeds.FirstOrDefault(x => x.Uid == source.BreedId);
        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Uid == source.PhotoGalleryID);

        destination.UserID = user.UserID;
        destination.CategoryID = category.Id;
        destination.BreedID = breed.Id;
        destination.PhotoGalleryID = photoGallery.Id;
    }
}