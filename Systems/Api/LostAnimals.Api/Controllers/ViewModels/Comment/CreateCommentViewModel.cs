using AutoMapper;
using LostAnimals.Services.Comments;

namespace LostAnimals.Api.Controllers.Models.Comment;

public class CreateCommentViewModel
{
    public Guid UserId { get; set; }

    public Guid NoteId { get; set; }

    public Guid? ParentCommentId { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}

public class CreateCommentViewModelProfile : Profile
{
    public CreateCommentViewModelProfile()
    {
        CreateMap<CreateCommentViewModel, CommentModel>();
    }
}