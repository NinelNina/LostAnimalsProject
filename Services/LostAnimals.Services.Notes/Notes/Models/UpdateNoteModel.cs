using AutoMapper;
using LostAnimals.Context.Entities;

namespace LostAnimals.Services.Notes;

public class UpdateNoteModel
{
    public Guid CategoryId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string? AnimalName { get; set; }

    public Guid BreedId { get; set; }

    public string Text { get; set; }

    public Guid PhotoGalleryID { get; set; }

    public string Region { get; set; }
    public string City { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public DateTime LastSeenDate { get; set; }
    public DateTime LastEditDate { get; set;}

    public string PhoneNumber { get; set; }
}

public class UpdateNoteProfile : Profile
{
    public UpdateNoteProfile()
    {
        CreateMap<UpdateNoteModel, Note>();
    }
}