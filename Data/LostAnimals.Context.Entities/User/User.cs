namespace LostAnimals.Context.Entities;

using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class User : IdentityUser<Guid>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UserID { get; set; }

/*    public string FullName { get; set; }

    public string Region { get; set; }
    public string City { get; set; }*/

    public virtual ICollection<Note>? Notes { get; set; }
    public virtual ICollection<Comment>? Comments { get; set; }
}
