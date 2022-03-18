namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class UserRoleController : ControllerBase
{
    private readonly IUserRoleService _UserRoleService;
    public UserRoleController(IUserRoleService UserRoleService)
    {
        _UserRoleService = UserRoleService;
    }

    [HttpPost("postinfo")]
    public async Task<ActionResult<UserRoleDTO>> CreateUserRole(string userName, int RoleId)
    {
        return await _UserRoleService.CreateUserRole(userName, RoleId);
    }

    [HttpDelete("deleteinfo")]
    public async Task DeleteUserRole(string Username, int RoleId)
    {
        await _UserRoleService.DeleteUserRole(Username, RoleId);
    }

    [HttpGet]
    public async Task<IEnumerable<UserRoleDTO>> GetAllUserRoles()
    {
        return await _UserRoleService.GetAllUserRoles();
    }
   
    [Route("Role/Email")]
    [HttpGet]   //https://localhost:7293/UserRole/Role/Email?Email=melek@gmail.com
    public async Task<IEnumerable<Role>> GetRolesOfAnUserByEmail(string Email)
    {
        return await _UserRoleService.GetRolesOfAnUserByEmail(Email);
    }

    [Route("Role/Id")]
    [HttpGet]
    public async Task<Role> GetRoleById(int Id)
    {
        return await _UserRoleService.GetRoleById(Id);
    }

    [Route("Role/Username")]
    [HttpGet]
    public async Task<IEnumerable<Role>> GetRolesOfUserByUsername(string Username)
    {
        return await _UserRoleService.GetRolesOfUserByUsername(Username);
    }
}