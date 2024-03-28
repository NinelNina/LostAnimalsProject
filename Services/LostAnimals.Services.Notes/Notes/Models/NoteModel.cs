using AutoMapper;
using LostAnimals.Context;
using LostAnimals.Context.Entities;
using LostAnimals.Services.Comments;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Services.Notes;

//TODO: создать модель для слоя представления в API
//TODO: добавить получение AnimalKind
//TODO: добавить комментарии
//TODO: добавить фото

public class NoteModel
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public string Username { get; set; }

    public Guid CategoryId { get; set; }
    public string CategoryName { get; set; }

    public string Title { get; set; }
    public string? AnimalName { get; set; }

    //public string AnimalKind { get; set; }

    public Guid BreedId { get; set; }
    public string AnimalBreed { get; set; }

    public string Content { get; set; }

    public Guid? PhotoGalleryId { get; set; }
    //public ICollection<PhotoStorage>? PhotoStorages { get; set; }

    public ICollection<CommentModel>? comments { get; set; }

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
        CreateMap<Note, NoteModel>()
            .BeforeMap<NoteModelActions>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CategoryId, opt => opt.Ignore())
            .ForMember(dest => dest.BreedId, opt => opt.Ignore())
            .ForMember(dest => dest.PhotoGalleryId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }

    public class NoteModelActions : IMappingAction<Note, NoteModel>
    {
        private readonly IDbContextFactory<MainDbContext> dbContextFactory;

        public NoteModelActions(IDbContextFactory<MainDbContext> dbContextFactory)
        {
            this.dbContextFactory = dbContextFactory;
        }

        public void Process(Note source, NoteModel destination, ResolutionContext context)
        {
            using var db = dbContextFactory.CreateDbContext();

            var note = db.Notes
                .Include(x => x.User)
                .Include(x => x.Category)
                //.Include(x => x.Comments)
                .Include(x => x.Breed)
                .Include(x => x.PhotoGallery)
                //.ThenInclude(x => x.PhotoStorages)
                .FirstOrDefault(x => x.Id == source.Id);

            destination.Id = note.Uid;
            destination.UserId = note.User.Id;
            destination.Username = note.User.UserName;
            destination.CategoryId = note.Category.Uid;
            destination.CategoryName = note.Category.CategoryName;
            destination.BreedId = note.Breed.Uid;
            destination.AnimalBreed = note.Breed.BreedName;

            if (note.PhotoGallery != null)
            {
                destination.PhotoGalleryId = note.PhotoGallery.Uid;
            }
            //destination.PhotoStorages = note.PhotoGallery.PhotoStorages;
        }
    }
}

