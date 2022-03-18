namespace Armut.Model
{
    public class Role
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public virtual IEnumerable<UserRole> UserRoles { get; set; }
        public virtual IEnumerable<User>? Users { get; set; }
    }
}
