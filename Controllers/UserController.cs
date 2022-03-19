namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _UserService;
    public UserController(IUserService UserService)
    {
        _UserService = UserService;
    }

    [HttpPost]
    public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO user)
    {
        return await _UserService.CreateUser(user);
    }

    [HttpPut("username")]
    public async Task<ActionResult<UserDTO>> UpdateUser(string userName, UpdateUserDTO userDTO)
    {
        return await _UserService.UpdateUser(userName, userDTO);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<UserDTO>> GetUserById(int Id)
    {
        return await _UserService.GetUserById(Id);
    }

    [HttpGet("username")]
    public async Task<ActionResult<UserDTO>> GetUserByUsername(string Username)
    {
        return await _UserService.GetUserByUsername(Username);
    }

    [HttpGet]
    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        return await _UserService.GetAllUsers();
    }

    [HttpGet("Age")] //https://localhost:7293/User/Age?minage=20&maxage=25
    public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsersByAgeLimits(string minAge, string maxAge)
    {
        return await _UserService.GetUsersByAgeLimits(minAge, maxAge);
    }

    [HttpGet("RoleName")] 
    public async Task<IEnumerable<UserDTO>> GetUsersByRoleName(string RoleName)
    {
        return await _UserService.GetUsersByRoleName(RoleName);
    }

    [HttpGet("GenderType")] //https://localhost:7293/User/gendertype?type=female
    public async Task<IEnumerable<UserDTO>> GetUsersByGender(string Type)
    {
        return await _UserService.GetUsersByGender(Type);
    }

    [HttpGet("CreatedActivityId")] 
    public async Task<ActionResult<UserDTO>> GetUserByCreatedActivityId(int Id)
    {
        return await _UserService.GetUserByCreatedActivityId(Id);
    }
}
