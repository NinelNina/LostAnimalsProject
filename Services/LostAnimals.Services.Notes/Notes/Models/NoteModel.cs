using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Notes;

//TODO: создать модель для слоя представления в API

public class NoteModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string Username { get; set; }

    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }

    public string Title { get; set; }
    public string Description { get; set; }
    public string? AnimalName { get; set; }
    public string AnimalKind { get; set; }
    public string AnimalBreed { get; set; }
    public string Text { get; set; }

    public Guid? PhotoGalleryID { get; set; }
    public ICollection<PhotoStorage>? PhotoStorages { get; set; }

    public string PhoneNumber { get; set; }

    public string Region { get; set; }
    public string City { get; set; }
    public double? Latitude { get; set; }
    public double? Longitude { get; set; }

    public DateTime LastSeenDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? LastEditDate { get; set; }
}

public class NoteModelProfile : Profile
{
    public NoteModelProfile()
    {
        CreateMap<Note, NoteModel>();
    }

    public class NoteModelActions : IMappingAction<Note, NoteModel>
    {
        private readonly IDbContextFactory<MainDbContext> dbContextFactory;

        public void Process(Note source, NoteModel destination, ResolutionContext context)
        {
            using var db = dbContextFactory.CreateDbContext();

            var note = db.Notes
                .Include(x => x.User)
                .Include(x => x.Category)
                .Include(x => x.Comments)
                .Include(x => x.PhotoGallery)
                .ThenInclude(x => x.PhotoStorages)
                .FirstOrDefault(x => x.Id == source.Id);

            destination.Id = note.Uid;
            destination.UserId = note.User.Id;
            destination.Username = note.User.UserName;
            destination.CategoryId = note.Category.Uid;
            destination.CategoryName = note.Category.CategoryName;
            destination.PhotoGalleryID = note.PhotoGallery.Uid;
            destination.PhotoStorages = note.PhotoGallery.PhotoStorages;
        }
    }
}

