public class UserRoleRepository : IUserRoleRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public UserRoleRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public UserRole CreateUserRole(string userName, int RoleId)
    {
        User user = _BaseDBContext.Users.FirstOrDefault(x => x.Username == userName);
        UserRole userRole = new UserRole()
        {
             UserId = user.Id,
             RoleId = RoleId
        };
        _BaseDBContext.UserRoles.Add(userRole);
        _BaseDBContext.SaveChanges();
        return userRole;
        
        
        /*
        User user = _BaseDBContext.Users.FirstOrDefault(x => x.Username == userName);
        Role role= _BaseDBContext.Roles.FirstOrDefault(x=>x.Id == RoleId);
        user.Roles.Add(role);
        _BaseDBContext.SaveChanges();
        return null;
        */
    }

    public void DeleteUserRole(string Username, int RoleId)
    {
        User user = _BaseDBContext.Users.FirstOrDefault(x => x.Username == Username);
        UserRole userRole = _BaseDBContext.UserRoles.Where(x => x.RoleId == RoleId && x.UserId == user.Id).FirstOrDefault();
        _BaseDBContext.UserRoles.Remove(userRole);
        _BaseDBContext.SaveChanges();
    }

    public IEnumerable<UserRole> GetAllUserRoles()
    {
         return _BaseDBContext.UserRoles.ToList();
    }

    public IEnumerable<Role> GetRolesOfAnUserByEmail(string Email)
    {
        User user = _BaseDBContext.Users.FirstOrDefault(x => x.Account.Email == Email);
        var userRoles = _BaseDBContext.UserRoles.Where(x=>x.UserId == user.Id).ToList();
        return (from userRole in userRoles
                select new Role
                {
                     Id = userRole.RoleId,
                     Name = (from x in _BaseDBContext.Roles where x.Id == userRole.RoleId select x.Name).FirstOrDefault()
                }).ToList();
    }

    public UserRole UpdateUserRole(string Username, int newRoleId, int oldRoleId)
    {
        User user = _BaseDBContext.Users.FirstOrDefault(x => x.Username == Username);
        UserRole userRole = _BaseDBContext.UserRoles.FirstOrDefault(x => x.RoleId == oldRoleId && x.UserId == user.Id);
        if(userRole == null) return null;
        userRole.RoleId = newRoleId;
        _BaseDBContext.SaveChanges(); //burada roleId key olduğu için değiştiremezsiniz diyor.
        return userRole;
    }

    public string GetUserName(int UserId)
    {
        return (from x in _BaseDBContext.Users where x.Id == UserId select x.Username).FirstOrDefault();
    }

    public int NumberOfAdmins()
    {
        var a = (from x in _BaseDBContext.UserRoles where x.Role.Name == "Admin" select x).ToList();
        return a.Count;
    }
    public int RoleNumberOfAnUser(string Username, int RoleId)
    {
        var a = (from x in _BaseDBContext.UserRoles where x.User.Username == Username select x.RoleId).ToList();
        return a.Count;
    }

    public Role GetRoleById(int Id)
    {
        return (from x in _BaseDBContext.Roles where x.Id == Id select x).FirstOrDefault();
    }

    public IEnumerable<Role> GetRolesOfUserByUsername(string Username)
    {
        return (from x in _BaseDBContext.UserRoles
                where x.User.Username == Username
                select new Role() { Id = x.RoleId, Name = x.Role.Name }).ToList();
    }

    public bool IsUsernameExist(string name)
    {
        return _BaseDBContext.Users.Any(x=>x.Username == name);
    }
}