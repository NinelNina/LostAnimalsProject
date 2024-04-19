using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Comments;

public class UpdateCommentModel
{
    public Guid? PhotoGalleryId { get; set; }
}

public class UpdateCommentModelProfile : Profile
{
    public UpdateCommentModelProfile()
    {
        CreateMap<UpdateCommentModel, Comment>()
            .ForMember(x => x.PhotoGalleryID, opt => opt.Ignore())
            .AfterMap<UpdateCommentModelActions>();

        CreateMap<Comment, UpdateCommentModel>()
            .ForMember(x => x.PhotoGalleryId, opt => opt.Ignore())
            .AfterMap<CommentToUpdateCommentModelActions>();
    }
}

public class UpdateCommentModelActions : IMappingAction<UpdateCommentModel, Comment>
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public UpdateCommentModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(UpdateCommentModel source, Comment destination, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Uid == source.PhotoGalleryId);

        if (photoGallery != null)
        {
            destination.PhotoGalleryID = photoGallery.Id;
        }
    }
}

public class CommentToUpdateCommentModelActions : IMappingAction<Comment, UpdateCommentModel>
{
    private readonly IDbContextFactory<MainDbContext> dbContextFactory;

    public CommentToUpdateCommentModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
    {
        this.dbContextFactory = dbContextFactory;
    }

    public void Process(Comment source, UpdateCommentModel destination, ResolutionContext context)
    {
        using var db = dbContextFactory.CreateDbContext();

        var photoGallery = db.PhotoGallery.FirstOrDefault(x => x.Id == source.PhotoGalleryID);

        if (photoGallery != null)
        {
            destination.PhotoGalleryId = photoGallery.Uid;
        }
    }
}