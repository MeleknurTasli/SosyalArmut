public interface IUserRoleRepository
{
    public UserRole CreateUserRole(string userName, int RoleId); 
    public UserRole UpdateUserRole(string Username, int newRoleId, int oldRoleId);
    public void DeleteUserRole(string Username, int RoleId);
    public IEnumerable<UserRole> GetAllUserRoles();
    public IEnumerable<Role> GetRolesOfAnUserByEmail(string Email);
    public Role GetRoleById(int Id);
    public IEnumerable<Role> GetRolesOfUserByUsername(string Username);



    public string GetUserName(int UserId);
    public int NumberOfAdmins();
    public int RoleNumberOfAnUser(string Username, int RoleId);


    public bool IsUsernameExist(string name);


}