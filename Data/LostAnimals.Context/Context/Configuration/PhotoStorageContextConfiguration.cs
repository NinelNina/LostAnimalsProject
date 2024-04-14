using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Context.Configuration
{
    public static class PhotoStorageContextConfiguration
    {
        public static void ConfigurePhotoStorage(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PhotoStorage>().
            ToTable("photo_storage");

            modelBuilder.Entity<PhotoStorage>()
                .Property(x => x.PhotoName)
                .IsRequired();

            modelBuilder.Entity<PhotoStorage>()
                .HasOne(x => x.PhotoGallery)
                .WithMany(x => x.PhotoStorages)
                .HasForeignKey(x => x.PhotoGalleryID)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
