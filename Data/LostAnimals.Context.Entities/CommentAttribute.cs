namespace LostAnimals.Context.Entities
{
    public class CommentAttribute : BaseEntity
    {
        public string AttributeName { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
    }
}