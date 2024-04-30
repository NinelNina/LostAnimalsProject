using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Notes;

public class UpdateNoteModel
{
    public string Title { get; set; }
    public string? AnimalName { get; set; }

    public Guid BreedId { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }

    public double? Latitude { get; set; }
    public double? Longtitude { get; set; }

    public bool IsActive { get; set; } = true;

    public string PhoneNumber { get; set; }

    public DateTime LastSeenDate { get; set; }
    public DateTime LastEditDate { get; set; }
}

public class UpdateNoteProfile : Profile
{
    public UpdateNoteProfile()
    {
        CreateMap<UpdateNoteModel, Note>()
            .ForMember(dest => dest.BreedID, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoGalleryID, opt => opt.Ignore())
            .AfterMap<UpdateNoteModelToNoteActions>();

        CreateMap<Note, UpdateNoteModel>()
            .ForMember(dest => dest.BreedId, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoGalleryId, opt => opt.Ignore())
            .AfterMap<NoteToUpdateNoteModelActions>();
    }
}

public class UpdateNoteModelToNoteActions : IMappingAction<UpdateNoteModel, Note>
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public UpdateNoteModelToNoteActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(UpdateNoteModel source, Note destination, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var breed = db.Breeds.FirstOrDefault(x => x.Uid == source.BreedId);
        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Uid == source.PhotoGalleryId);

        destination.BreedID = breed.Id;

        if (photoGallery != null)
        {
            destination.PhotoGalleryID = photoGallery.Id;
        }
    }
}

public class NoteToUpdateNoteModelActions : IMappingAction<Note, UpdateNoteModel>
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public NoteToUpdateNoteModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(Note source, UpdateNoteModel destination, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var breed = db.Breeds.FirstOrDefault(x => x.Id == source.BreedID);
        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Id == source.PhotoGalleryID);

        destination.BreedId = breed.Uid;

        if (photoGallery != null)
        {
            destination.PhotoGalleryId = photoGallery.Uid;
        }
    }
}