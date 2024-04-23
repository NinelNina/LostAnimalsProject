namespace LostAnimals.Context.Entities;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class User : IdentityUser<Guid>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }
    public virtual ICollection<Note>? Notes { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
}
