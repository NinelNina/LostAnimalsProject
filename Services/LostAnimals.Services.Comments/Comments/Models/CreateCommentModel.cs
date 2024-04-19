using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Comments;

public class CreateCommentModel
{
    public Guid UserId { get; set; }

    public Guid NoteId { get; set; }

    public Guid? ParentCommentId { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }

    public DateTime CreatedDate { get; set; }
}

public class CreateCommentModelProfile : Profile
{
    public CreateCommentModelProfile()
    {
        CreateMap<CreateCommentModel, Comment>()
            .ForMember(x => x.UserID, opt => opt.Ignore())
            .ForMember(x => x.NoteID, opt => opt.Ignore())
            .ForMember(x => x.ParentCommentID, opt => opt.Ignore())
            .ForMember(x => x.PhotoGalleryID, opt => opt.Ignore())
            .AfterMap<CreateCommentModelActions>();
    }
}

public class CreateCommentModelActions : IMappingAction<CreateCommentModel, Comment>
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public CreateCommentModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(CreateCommentModel source, Comment destination, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var user = db.Users.FirstOrDefault(x => x.Id == source.UserId);
        var note = db.Notes.FirstOrDefault(x => x.Uid == source.NoteId);
        var parentComment = db.Comments.FirstOrDefault(x => x.Uid == source.ParentCommentId);
        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Uid == source.PhotoGalleryId);

        destination.UserID = user.UserID;
        destination.NoteID = note.Id;

        if (parentComment != null)
        {
            destination.ParentCommentID = parentComment.Id;
        }

        if (photoGallery != null)
        {
            destination.PhotoGalleryID = photoGallery.Id;
        }
    }
}