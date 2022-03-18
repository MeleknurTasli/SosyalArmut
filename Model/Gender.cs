namespace Armut.Model
{
    public class Gender
    {
        public int Id { get; set; }
        public string? Type { get; set; }

        public virtual IEnumerable<User> Users {get; set;}
    }
}
