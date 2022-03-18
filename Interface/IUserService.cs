public interface IUserService
{
    public Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO user);
    public Task<ActionResult<UserDTO>> UpdateUser(string userName, UpdateUserDTO userDTO);
    public Task<ActionResult<UserDTO>> GetUserById(int Id);
    public Task<ActionResult<UserDTO>> GetUserByUsername(string Username);
    public Task<IEnumerable<UserDTO>> GetAllUsers();
    public Task<IEnumerable<UserDTO>> GetUsersByAgeLimits(string minAge, string maxAge);
    public Task<IEnumerable<UserDTO>> GetUsersByRoleName(string RoleName);
    public Task<IEnumerable<UserDTO>> GetUsersByGender(string Type);
    public Task<ActionResult<UserDTO>> GetUserByCreatedActivityId(int Id);
}