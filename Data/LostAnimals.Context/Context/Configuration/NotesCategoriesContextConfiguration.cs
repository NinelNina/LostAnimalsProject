using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Context.Configuration;

public static class NotesCategoriesContextConfiguration
{
    public static void ConfigureNotesCategories(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<NoteCategory>().
            ToTable("note_categories");

        modelBuilder.Entity<NoteCategory>()
            .Property(x => x.CategoryName)
            .HasMaxLength(100)
            .IsRequired();
    }
}
