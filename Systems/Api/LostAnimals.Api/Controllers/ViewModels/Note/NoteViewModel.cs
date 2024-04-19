using AutoMapper;
using LostAnimals.Api.Controllers.Models.Comment;
using LostAnimals.Services.Notes;

namespace LostAnimals.Api.Controllers.Models.Note;

public class NoteViewModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string Username { get; set; }

    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }

    public string Title { get; set; }
    public string? AnimalName { get; set; }

    public string AnimalKind { get; set; }

    public Guid BreedId { get; set; }
    public string AnimalBreed { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }

    public string PhoneNumber { get; set; }

    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public DateTime LastSeenDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastEditDate { get; set; }

    public ICollection<CommentViewModel>? Comments { get; set; }
}

public class NoteViewModelProfile : Profile
{
    public NoteViewModelProfile()
    {
        CreateMap<NoteModel, NoteViewModel>();
    }
}