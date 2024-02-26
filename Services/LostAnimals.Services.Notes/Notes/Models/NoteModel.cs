namespace LostAnimals.Services.Notes;

public class NoteModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string Username { get; set; }

    public Guid CategoryName { get; set; }
    public string CategoryId { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string? AnimalName { get; set; }
    public string AnimalKind { get; set; }
    public string AnimalBreed { get; set; }
    public string Text { get; set; }

    public Guid PhotoGalleryID { get; set; }
    public string PhotoName { get; set; }

    public string PhoneNumber { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public DateTime LastSeenDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastEditDate { get; set; }
}
