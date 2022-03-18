public interface IUserRepository
{
    public User CreateUser(CreateUserDTO user);
    public User UpdateUser(string Username, UpdateUserDTO userDTO);
    public User GetUserById(int Id);
    public User GetUserByUsername(string Username);
    public IEnumerable<User> GetAllUsers();
    public IEnumerable<User> GetUsersByAgeLimits(int minAge, int maxAge);
    public IEnumerable<User> GetUsersByRoleName(string RoleName);
    public IEnumerable<User> GetUsersByGender(string Type);
    public User GetUserByCreatedActivityId(int Id);
}