public class UserRoleService : ControllerBase, IUserRoleService
{
    private readonly IUserRoleRepository _UserRoleRepository;
    public UserRoleService(IUserRoleRepository UserRoleRepository)
    {
        _UserRoleRepository = UserRoleRepository;
    }

    public async Task<ActionResult<UserRoleDTO>> CreateUserRole(string userName, int RoleId)
    {
        try
        {
            if(!_UserRoleRepository.IsUsernameExist(userName) || _UserRoleRepository.GetRoleById(RoleId) == null) 
                return BadRequest("RoleId ya da username mevcut değildir.");
            bool value = true;
            foreach (var item in _UserRoleRepository.GetRolesOfUserByUsername(userName))
            {
                if (item.Name == _UserRoleRepository.GetRoleById(RoleId).Name)
                {
                    value = false;
                    break;
                }
            }
            if(value)
            {
                UserRole userRole = _UserRoleRepository.CreateUserRole(userName, RoleId);
                return ConvertToUserRoleDTO(userRole);
            }
            return BadRequest("Bu kullanıcı bu role zaten sahiptir.");
        }
        catch
        {
            return null;
        }
    }

    public async Task DeleteUserRole(string Username, int RoleId)
    {
        try
        {
            bool isLastAdmin = _UserRoleRepository.NumberOfAdmins() == 1 ? true : false;
            if(_UserRoleRepository.RoleNumberOfAnUser(Username, RoleId) > 1 && !isLastAdmin)
            {
                _UserRoleRepository.DeleteUserRole(Username, RoleId);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<UserRoleDTO>> GetAllUserRoles()
    {
        try
        {
            return ConvertToUserRoleDTOList(_UserRoleRepository.GetAllUserRoles());
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<Role>> GetRolesOfAnUserByEmail(string Email)
    {
        try
        {
            return _UserRoleRepository.GetRolesOfAnUserByEmail(Email);
        }
        catch
        {
            return null;
        }
    }

    public async Task<ActionResult<UserRoleDTO>> UpdateUserRole(string Username, int newRoleId, int oldRoleId)
    {
        try
        {
            if(!_UserRoleRepository.IsUsernameExist(Username) || _UserRoleRepository.GetRoleById(newRoleId) == null || _UserRoleRepository.GetRoleById(oldRoleId) == null) 
                return BadRequest("RoleId ya da username mevcut değildir.");
            if (_UserRoleRepository.NumberOfAdmins() <= 1 && _UserRoleRepository.GetRoleById(oldRoleId).Name == "Admin")
            {
                return BadRequest("Başka admin kalmayacak.");;
            }
            else
            {
                bool value = true;
                foreach (Role item in _UserRoleRepository.GetRolesOfUserByUsername(Username))
                {
                    if (item.Name == _UserRoleRepository.GetRoleById(newRoleId).Name)
                    {
                        value = false;
                        break;
                    }
                }
                if (value)
                {
                    UserRole userRole = _UserRoleRepository.UpdateUserRole(Username, newRoleId, oldRoleId);
                    if(userRole == null) return BadRequest("Bu kullanıcı girilen eski role sahip değil.");
                    return ConvertToUserRoleDTO(userRole);
                }
                return BadRequest("Bu kullanıcı bu role zaten sahiptir.");;
            }
        }
        catch
        {
            throw;
        }
    }
    public async Task<Role> GetRoleById(int Id)
    {
        return _UserRoleRepository.GetRoleById(Id);
    }
    public async Task<IEnumerable<Role>> GetRolesOfUserByUsername(string Username)
    {
        return _UserRoleRepository.GetRolesOfUserByUsername(Username);
    }





    private List< UserRoleDTO> ConvertToUserRoleDTOList(IEnumerable<UserRole>  UserRoles)
    {
        List<UserRoleDTO>  UserRoleDTOs = new List<UserRoleDTO>();
        foreach( UserRole userRole in  UserRoles)
        {
             UserRoleDTOs.Add(new UserRoleDTO(_UserRoleRepository.GetUserName(userRole.UserId), userRole));
        }
        return  UserRoleDTOs;
    }

    private UserRoleDTO ConvertToUserRoleDTO(UserRole userRole)
    {
        return new UserRoleDTO(_UserRoleRepository.GetUserName(userRole.UserId), userRole);
    }
}