using LostAnimals.Context.Configuration;
using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Context;

public class MainDbContext : DbContext
{
    public DbSet<Note> Notes {  get; set; }
    public DbSet<NoteCategory> NoteCategories { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Entities.CommentAttribute> CommentAttributes { get; set; }
    public DbSet<PhotoStorage> PhotoStorage { get; set; }
    public DbSet<PhotoGallery> PhotoGallery { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<AnimalKind> AnimalKinds { get; set; }

    public MainDbContext(DbContextOptions<MainDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureBreeds();
        modelBuilder.ConfigureAnimalKinds();
        modelBuilder.ConfigureComments();
        modelBuilder.ConfigureCommentAttributes();
        modelBuilder.ConfigureNotes();
        modelBuilder.ConfigureNotesCategories();
        modelBuilder.ConfigurePhotoStorage();
        modelBuilder.ConfigurePhotoGallery();
    }
}
