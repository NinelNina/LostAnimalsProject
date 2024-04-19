using AutoMapper;
using LostAnimals.Services.Notes;

namespace LostAnimals.Api.Controllers.Models.Note;

public class CreateNoteViewModel
{
    public Guid UserId { get; set; }

    public Guid CategoryId { get; set; }

    public string Title { get; set; }

    public string? AnimalName { get; set; }

    public Guid BreedId { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }

    public double? Latitude { get; set; }

    public double? Longitude { get; set; }

    public bool IsActive { get; set; } = true;

    public string PhoneNumber { get; set; }

    public DateTime LastSeenDate { get; set; }

    public DateTime CreatedDate { get; set; } = DateTime.Now;
}

public class CreateNoteViewModelProfile : Profile
{
    public CreateNoteViewModelProfile()
    {
        CreateMap<CreateNoteViewModel, NoteModel>();
    }
}