using Microsoft.EntityFrameworkCore;
using LostAnimals.Context.Entities;

namespace LostAnimals.Context.Configuration;

public static class NotesContextConfiguration
{
    public static void ConfigureNotes(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Note>()
            .ToTable("notes");

        modelBuilder.Entity<Note>()
            .Property(x => x.Content)
            .IsRequired();

        modelBuilder.Entity<Note>()
            .Property(x => x.Title)
            .HasMaxLength(250)
            .IsRequired();

        modelBuilder.Entity<Note>()
            .Property(x => x.CreatedDate)
            .IsRequired();
        
        modelBuilder.Entity<Note>()
            .Property(x => x.LastSeenDate)
            .IsRequired();
        
        modelBuilder.Entity<Note>()
            .Property(x => x.IsActive)
            .IsRequired();
        
        modelBuilder.Entity<Note>()
            .HasOne(x => x.Category)
            .WithMany(x => x.Notes)
            .HasForeignKey(x => x.CategoryID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        modelBuilder.Entity<Note>()
            .HasOne(x => x.Breed)
            .WithMany(x => x.Notes)
            .HasForeignKey(x => x.BreedID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
        
        modelBuilder.Entity<Note>()
            .HasOne(x => x.PhotoGallery)
            .WithMany(x => x.Notes)
            .HasForeignKey(x => x.PhotoGalleryID)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);
        
        modelBuilder.Entity<Note>()
            .HasOne(x => x.User)
            .WithMany(u => u.Notes)
            .HasForeignKey(x => x.UserID)
            .HasPrincipalKey(u => u.UserID)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
