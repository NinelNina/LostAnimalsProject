namespace LostAnimals.Web.Pages.Comments.Models;

public class CreateCommentViewModel
{
    public Guid UserId { get; set; }

    public Guid NoteId { get; set; }

    public Guid? ParentCommentId { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}