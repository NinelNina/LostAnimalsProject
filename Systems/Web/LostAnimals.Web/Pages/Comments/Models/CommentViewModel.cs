namespace LostAnimals.Web.Pages.Comments.Models;

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