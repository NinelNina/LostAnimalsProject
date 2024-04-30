using AutoMapper;
using LostAnimals.Services.Notes;

namespace LostAnimals.ViewModels.Note;

public class UpdateNoteViewModel
{
    public string Title { get; set; }

    public string? AnimalName { get; set; }

    public Guid BreedId { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }

    public double? Latitude { get; set; }

    public double? Longtitude { get; set; }

    public bool IsActive { get; set; } = true;

    public string PhoneNumber { get; set; }

    public DateTime LastSeenDate { get; set; }

    public DateTime LastEditDate { get; set; } = DateTime.Now;
}

public class UpdateNoteViewProfile : Profile
{
    public UpdateNoteViewProfile()
    {
        CreateMap<UpdateNoteViewModel, NoteModel>();
    }
}