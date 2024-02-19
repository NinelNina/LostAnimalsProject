using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Context.Configuration;

public static class BreedsContextConfiguration
{
    public static void ConfigureBreeds(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Breed>().
            ToTable("breeds");

        modelBuilder.Entity<Breed>()
            .Property(x => x.BreedName)
            .IsRequired();

        modelBuilder.Entity<Breed>()
            .HasOne(x => x.AnimalKind)
            .WithMany(x => x.Breeds)
            .HasForeignKey(x => x.AnimalKindID)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
