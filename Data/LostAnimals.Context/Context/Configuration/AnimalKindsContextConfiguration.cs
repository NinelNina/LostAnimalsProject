using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Context.Configuration
{
    public static class AnimalKindsContextConfiguration
    {
        public static void ConfigureAnimalKinds(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalKind>().
                ToTable("animal_kinds");

            modelBuilder.Entity<AnimalKind>()
                .Property(x => x.AnimalKindName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
