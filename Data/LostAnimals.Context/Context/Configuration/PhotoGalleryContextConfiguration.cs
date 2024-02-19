using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Context.Configuration;

public static class PhotoGalleryContextConfiguration
{
    public static void ConfigurePhotoGallery(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PhotoGallery>().
        ToTable("photo_gallery");
    }
}
