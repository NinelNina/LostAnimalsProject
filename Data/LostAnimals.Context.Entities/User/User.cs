namespace LostAnimals.Context.Entities;

using Microsoft.AspNetCore.Identity;

public class User : IdentityUser<Guid>
{
    public string FullName { get; set; }
    //public IdentityRole Role { get; set; }

    public virtual ICollection<Note> Notes { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }
}
