using AutoMapper;
using LostAnimals.Services.Comments;

namespace LostAnimals.Api.Controllers.Models.Comment;

public class CommentViewModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public string UserName { get; set; }

    public Guid NoteId { get; set; }

    public Guid? ParentCommentId { get; set; }

    public string Content { get; set; }

    public DateTime CreatedDate { get; set; }

    public Guid? PhotoGalleryId { get; set; }
}

public class CommentViewModelProfile : Profile
{
    public CommentViewModelProfile()
    {
        CreateMap<CommentModel, CommentViewModel>();
    }
}