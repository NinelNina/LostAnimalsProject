using AutoMapper;
using LostAnimals.Context.Entities;
using LostAnimals.Context;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Comments;

public class CommentModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string UserName { get; set; }

    public Guid NoteId { get; set; }
    //public Note Note { get; set; }

    public Guid? ParentCommentId { get; set; }
    //public Comment? ParentComment { get; set; }

    public string Content { get; set; }
    public DateTime CreatedDate { get; set; }

    public Guid? PhotoGalleryId { get; set; }
}

public class CommentModelProfile : Profile
{
    public CommentModelProfile()
    {
        CreateMap<Comment, CommentModel>()
            .BeforeMap<CommentModelActions>()
            .ForMember(x => x.UserId, opt => opt.Ignore())
            .ForMember(x => x.NoteId, opt => opt.Ignore())
            .ForMember(x => x.ParentCommentId, opt => opt.Ignore())
            .ForMember(x => x.PhotoGalleryId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}

public class CommentModelActions : IMappingAction<Comment, CommentModel>
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public CommentModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(Comment source, CommentModel destination, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var comment = db.Comments
                .Include(x => x.User)
                .Include(x => x.Note)
                .Include(x => x.ParentComment)
                .Include(x => x.PhotoGallery)
                .FirstOrDefault(x => x.Id == source.Id);

        destination.Id = comment.Uid;
        destination.UserId = comment.User.Id;
        destination.UserName = comment.User.UserName;
        destination.NoteId = comment.Note.Uid;

        if (comment.ParentComment != null)
        {
            destination.ParentCommentId = comment.ParentComment.Uid;
        }

        if (comment.PhotoGallery != null)
        {
            destination.ParentCommentId = comment.PhotoGallery.Uid;
        }
    }
}