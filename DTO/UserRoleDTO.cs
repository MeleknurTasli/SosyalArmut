public class UserRoleDTO
{
    public int? RoleId { get; set; }
    public string userName {get; set;}
    //public virtual User? User { get; set; }
    //public virtual Role? Role { get; set; }
    public UserRoleDTO(string userName, UserRole UserRole)
    {
        RoleId = UserRole.RoleId;
        //User = UserRole.User;
        //Role = UserRole.Role;
        this.userName = userName;
    }
}