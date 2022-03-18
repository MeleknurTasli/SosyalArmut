public class UserService : ControllerBase, IUserService
{
    private readonly IUserRepository _UserRepository;
    public UserService(IUserRepository _UserRepository)
    {
        this._UserRepository = _UserRepository;
    }

    public async Task<ActionResult<UserDTO>> CreateUser(CreateUserDTO user)
    {
        try
        {
            if(_UserRepository.GetUserByUsername(user.Username) == null)
            {
                var user_ = _UserRepository.CreateUser(user);
                if(user_!= null)
                {
                     return new UserDTO(user_);
                }
                return BadRequest("Girilen role Id'leri mevcut değildir.");
            }
            return BadRequest("Girilen username mevcuttur.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        try
        {
            return ConvertToUserDTO(_UserRepository.GetAllUsers());
        }
        catch
        {
            return null;
        }
    }

    public async Task<ActionResult<UserDTO>> GetUserByCreatedActivityId(int Id)
    {
        try
        {
            var user =_UserRepository.GetUserByCreatedActivityId(Id);
            if(user == null) return BadRequest("Mevcut değildir.");
            return new UserDTO(user);
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<UserDTO>> GetUsersByGender(string Type)
    {
        try
        {
            return ConvertToUserDTO(_UserRepository.GetUsersByGender(Type));
        }
        catch
        {
            return null;
        }
    }

    public async Task<ActionResult<UserDTO>> GetUserById(int Id)
    {
        try
        {
            var user = _UserRepository.GetUserById(Id);
            if(user == null) return BadRequest("Mevcut değildir.");
            return new UserDTO(user);
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<UserDTO>> GetUsersByRoleName(string RoleName)
    {
        try
        {
            return ConvertToUserDTO(_UserRepository.GetUsersByRoleName(RoleName));
        }
        catch
        {
            return null;
        }
    }

    public async Task<ActionResult<UserDTO>> GetUserByUsername(string Username)
    {
        try
        {
            var user = _UserRepository.GetUserByUsername(Username);
            if(user == null) return BadRequest("Mevcut değildir.");
            return new UserDTO(user);
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<UserDTO>> GetUsersByAgeLimits(string minAge, string maxAge)
    {
        try
        {
             int min, max;
             bool IsMinInt = Int32.TryParse(minAge, out min);
             bool IsMaxInt = Int32.TryParse(maxAge, out max);
             if(IsMaxInt && IsMinInt)
             {
                  return ConvertToUserDTO(_UserRepository.GetUsersByAgeLimits(min, max));
             }
             return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<ActionResult<UserDTO>> UpdateUser(string userName, UpdateUserDTO userDTO)
    {
        try
        {
            if(_UserRepository.GetUserByUsername(userName) != null )
            {
                if(userDTO.Username == userName || 
                      (userDTO.Username != userName && _UserRepository.GetUserByUsername(userDTO.Username) == null))
                {
                    var user = _UserRepository.UpdateUser(userName, userDTO);
                    return new UserDTO(user);
                }
                else return BadRequest("Girilen Username kullanılmaktadır.");;
            }
            else return BadRequest("Username mevcut değildir.");
        }
        catch
        {
            return null;
        }
    }

    private List<UserDTO> ConvertToUserDTO(IEnumerable<User> Users)
    {
        List<UserDTO> UserDTOs = new List<UserDTO>();
        foreach(User User in Users)
        {
            UserDTOs.Add(new UserDTO(User));
        }
        return UserDTOs;
    }
}