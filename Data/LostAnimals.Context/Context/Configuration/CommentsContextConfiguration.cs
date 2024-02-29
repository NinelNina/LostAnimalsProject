using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Context.Configuration;

public static class CommentsContextConfiguration
{
    public static void ConfigureComments(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comment>().
            ToTable("comments");

        modelBuilder.Entity<Comment>()
            .Property(x => x.Content)
            .IsRequired();

        modelBuilder.Entity<Comment>()
            .Property(x => x.CreatedDate)
            .IsRequired()
            .HasDefaultValue(DateTime.Now);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.Note)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.NoteID)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.PhotoGallery)
            .WithMany(x => x.Comments)
            .HasForeignKey(x => x.PhotoGalleryID)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.ParentComment)
            .WithOne()
            .HasForeignKey<Comment>(x => x.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired(false);

        modelBuilder.Entity<Comment>()
            .HasOne(x => x.User)
            .WithMany(u => u.Comments)
            .HasForeignKey(x => x.UserID)
            .HasPrincipalKey(u => u.UserID)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
