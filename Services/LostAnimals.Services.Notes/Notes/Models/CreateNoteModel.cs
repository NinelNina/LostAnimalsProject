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

    public Guid PhotoGalleryID { get; set; }

    public string Region { get; set; }
    public string City { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public DateTime LastSeenDate { get; set; }
    public DateTime CreatedDate { get; set; }
}


public class CreateNoteModelProfile : Profile
{
    public CreateNoteModelProfile()
    {
        CreateMap<CreateNoteModel, Note>()
            .ForMember(dest => dest.UserID, opt => opt.Ignore())
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

        destination.UserID = user.UserID;
    }
}