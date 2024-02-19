using LostAnimals.Context.Entities;
using Microsoft.EntityFrameworkCore;

namespace LostAnimals.Context.Configuration
{
    public static class CommentAttributesContextConfiguration
    {
        public static void ConfigureCommentAttributes(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Entities.CommentAttribute>().
                ToTable("comment_attributes");

            modelBuilder.Entity<Entities.CommentAttribute>()
                .Property(x => x.AttributeName)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}
