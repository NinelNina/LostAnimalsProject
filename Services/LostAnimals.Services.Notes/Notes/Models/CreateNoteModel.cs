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

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public DateTime LastSeenDate { get; set; }
    public DateTime CreatedDate { get; set; }
}
