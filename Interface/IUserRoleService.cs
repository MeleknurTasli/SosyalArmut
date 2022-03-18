public interface IUserRoleService
{
    public Task<ActionResult<UserRoleDTO>> CreateUserRole(string userName, int RoleId); 
    public Task<ActionResult<UserRoleDTO>> UpdateUserRole(string Username, int newRoleId, int oldRoleId);
    public Task DeleteUserRole(string Username, int RoleId);
    public Task<IEnumerable<UserRoleDTO>> GetAllUserRoles();
    public Task<IEnumerable<Role>> GetRolesOfAnUserByEmail(string Email);
    public Task<Role> GetRoleById(int Id);
    public Task<IEnumerable<Role>> GetRolesOfUserByUsername(string Username);
}