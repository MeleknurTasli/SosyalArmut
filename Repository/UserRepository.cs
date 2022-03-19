public class UserRepository : IUserRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public UserRepository(BaseDBContext _BaseDBContext)
    {
        this._BaseDBContext = _BaseDBContext;
    }

    public User CreateUser(CreateUserDTO createUserDTO)
    {
        List<Role> roles =  _BaseDBContext.Roles.ToList();
        List<int> roleIds = new List<int>();
        foreach(var role in roles)
        {
            roleIds.Add(role.Id);
        }
        foreach(int roleId in createUserDTO.RoleIds)
        {
            if(!roleIds.Contains(roleId))
            {
                return null;
            }
        }
        if(!roleIds.Contains(1))  roleIds.Add(1);//çünkü her kaydolan aynı zamanda user
        User user = new User(createUserDTO);
        _BaseDBContext.Users.Add(user);
        _BaseDBContext.SaveChanges();
        user = _BaseDBContext.Users.FirstOrDefault(x=>x.Username == user.Username);
        foreach(int roleId in createUserDTO.RoleIds)
        {
           _BaseDBContext.UserRoles.Add(new UserRole(){ UserId = user.Id, RoleId = roleId});
        }
        _BaseDBContext.SaveChanges();
        return GetUserByUsername(user.Username);
    }

    public IEnumerable<User> GetAllUsers()
    {
         return (from x in _BaseDBContext.Users
        // join u in _BaseDBContext.UserRoles on x.Id equals u.UserId
        // join w in _BaseDBContext.WishedActivites on x.Id equals w.UserId
                 where x.Account.Visibility == true
                 select new User()
                 {
                     Id = x.Id,
                     Username = x.Username,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     BirthDay = x.BirthDay,
                     ProfilePhoto = x.ProfilePhoto,
                     PhoneNumber = x.PhoneNumber,
                     Account = new Account(){Email=x.Account.Email},
                     GenderId = x.GenderId,
                     Gender = new Gender(){ Id=x.GenderId, Type = x.Gender.Type },
                     UserRoles = (from u in _BaseDBContext.UserRoles where u.UserId == x.Id select new UserRole(){RoleId=u.RoleId}).ToList(),
                     CreatedActivities = (from ca in _BaseDBContext.Activities
                                          where ca.OwnerUserId == x.Id select ca).ToList(),
                     WishedActivities = (from w in _BaseDBContext.WishedActivites where w.UserId == x.Id select w).ToList(),
                     //UserActivityTimeTables = (from t in _BaseDBContext.UserActivityTimeTables where t.UserId == x.Id select t).ToList()
                     RecordedActivities = x.RecordedActivities
                 }
                ).ToList();
    }

    public User GetUserByCreatedActivityId(int Id)
    {
        int userId = 0;
        foreach(var item in (from x in _BaseDBContext.Activities select x).ToList())
        {
            if(item.Id == Id)
            {
                 userId = item.OwnerUserId;
                 break;
            }
        }
        return GetUserById(userId);
/*
         var user = (from x in _BaseDBContext.Users
        // join u in _BaseDBContext.UserRoles on x.Id equals u.UserId
        // join w in _BaseDBContext.WishedActivites on x.Id equals w.UserId
        join a in _BaseDBContext.Activities on x.Id equals a.OwnerUserId
                 where a.Id == Id
                 select new User()
                 {
                     Id = x.Id,
                     Username = x.Username,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     Age = x.Age,
                     ProfilePhoto = x.ProfilePhoto,
                     PhoneNumber = x.PhoneNumber,
                     Account = new Account(){Email=x.Account.Email},
                     GenderId = x.GenderId,
                     Gender = new Gender(){ Id=x.GenderId, Type = x.Gender.Type },
                     UserRoles = (from u in _BaseDBContext.UserRoles where u.UserId == x.Id select u).ToList(),
                     CreatedActivities = (from ca in _BaseDBContext.Activities
                                          where ca.OwnerUserId == x.Id select ca).ToList(),
                     WishedActivities = (from w in _BaseDBContext.WishedActivites where w.UserId == x.Id select w).ToList(),
                     
                 }
                ).FirstOrDefault();
                return user;
*/
    }

    public IEnumerable<User> GetUsersByGender(string Type)
    {
         var users = (from x in _BaseDBContext.Users
        // join u in _BaseDBContext.UserRoles on x.Id equals u.UserId
        // join w in _BaseDBContext.WishedActivites on x.Id equals w.UserId
                 where x.Gender.Type == Type && x.Account.Visibility == true
                 select new User()
                 {
                     Id = x.Id,
                     Username = x.Username,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     BirthDay = x.BirthDay,
                     ProfilePhoto = x.ProfilePhoto,
                     PhoneNumber = x.PhoneNumber,
                     Account = new Account(){Email=x.Account.Email},
                     GenderId = x.GenderId,
                     Gender = new Gender(){ Id=x.GenderId, Type = x.Gender.Type },
                     UserRoles = (from u in _BaseDBContext.UserRoles where u.UserId == x.Id select new UserRole(){RoleId=u.RoleId}).ToList(),
                     CreatedActivities = (from ca in _BaseDBContext.Activities
                                          where ca.OwnerUserId == x.Id select ca).ToList(),
                     WishedActivities = (from w in _BaseDBContext.WishedActivites where w.UserId == x.Id select w).ToList(),
                     //UserActivityTimeTables = (from t in _BaseDBContext.UserActivityTimeTables where t.UserId == x.Id select t).ToList()
                     RecordedActivities = x.RecordedActivities
                 }
                ).ToList();
                return users;
    }

    public User GetUserById(int Id)
    {
          var user = (from x in _BaseDBContext.Users
        // join u in _BaseDBContext.UserRoles on x.Id equals u.UserId
        // join w in _BaseDBContext.WishedActivites on x.Id equals w.UserId
                 where x.Id == Id
                 select new User()
                 {
                     Id = x.Id,
                     Username = x.Username,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     BirthDay = x.BirthDay,
                     ProfilePhoto = x.ProfilePhoto,
                     PhoneNumber = x.PhoneNumber,
                     Account = new Account(){Email=x.Account.Email},
                     GenderId = x.GenderId,
                     Gender = new Gender(){ Id=x.GenderId, Type = x.Gender.Type },
                     UserRoles = (from u in _BaseDBContext.UserRoles where u.UserId == x.Id select new UserRole(){RoleId=u.RoleId}).ToList(),
                     CreatedActivities = (from ca in _BaseDBContext.Activities
                                          where ca.OwnerUserId == x.Id select ca).ToList(),
                     WishedActivities = (from w in _BaseDBContext.WishedActivites where w.UserId == x.Id select w).ToList(),
                     //UserActivityTimeTables = (from t in _BaseDBContext.UserActivityTimeTables where t.UserId == x.Id select t).ToList()
                     RecordedActivities = x.RecordedActivities
                 }
                ).FirstOrDefault();
                return user;
    }

    public IEnumerable<User> GetUsersByRoleName(string RoleName)
    {
         var users = (from x in _BaseDBContext.Users
         join u in _BaseDBContext.UserRoles on x.Id equals u.UserId
        // join w in _BaseDBContext.WishedActivites on x.Id equals w.UserId
                 where u.Role.Name == RoleName && x.Account.Visibility == true
                 select new User()
                 {
                     Id = x.Id,
                     Username = x.Username,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     BirthDay = x.BirthDay,
                     ProfilePhoto = x.ProfilePhoto,
                     PhoneNumber = x.PhoneNumber,
                     Account = new Account(){Email=x.Account.Email},
                     GenderId = x.GenderId,
                     Gender = new Gender(){ Id=x.GenderId, Type = x.Gender.Type },
                     UserRoles = (from u in _BaseDBContext.UserRoles where u.UserId == x.Id select new UserRole(){RoleId=u.RoleId}).ToList(),
                     CreatedActivities = (from ca in _BaseDBContext.Activities
                                          where ca.OwnerUserId == x.Id select ca).ToList(),
                     WishedActivities = (from w in _BaseDBContext.WishedActivites where w.UserId == x.Id select w).ToList(),
                     //UserActivityTimeTables = (from t in _BaseDBContext.UserActivityTimeTables where t.UserId == x.Id select t).ToList()
                     RecordedActivities = x.RecordedActivities
                 }
                ).ToList();
                return users;
    }

    public User GetUserByUsername(string Username)
    {
         var user = (from x in _BaseDBContext.Users
        // join u in _BaseDBContext.UserRoles on x.Id equals u.UserId
        // join w in _BaseDBContext.WishedActivites on x.Id equals w.UserId
                 where x.Username == Username && x.Account.Visibility == true
                 select new User()
                 {
                     Id = x.Id,
                     Username = x.Username,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     BirthDay = x.BirthDay,
                     ProfilePhoto = x.ProfilePhoto,
                     PhoneNumber = x.PhoneNumber,
                     Account = new Account(){Email=x.Account.Email},
                     GenderId = x.GenderId,
                     Gender = new Gender(){ Id=x.GenderId, Type = x.Gender.Type },
                     UserRoles = (from u in _BaseDBContext.UserRoles where u.UserId == x.Id select new UserRole(){RoleId=u.RoleId}).ToList(),
                     CreatedActivities = (from ca in _BaseDBContext.Activities
                                          where ca.OwnerUserId == x.Id select ca).ToList(),
                     WishedActivities = (from w in _BaseDBContext.WishedActivites where w.UserId == x.Id select w).ToList(),
                     //UserActivityTimeTables = (from t in _BaseDBContext.UserActivityTimeTables where t.UserId == x.Id select t).ToList()
                     RecordedActivities = x.RecordedActivities
                 }
                ).FirstOrDefault();
                return user;
    }

    public IEnumerable<User> GetUsersByAgeLimits(int minAge, int maxAge)
    {
        var users = (from x in _BaseDBContext.Users
        // join u in _BaseDBContext.UserRoles on x.Id equals u.UserId
        // join w in _BaseDBContext.WishedActivites on x.Id equals w.UserId
                 where (DateTime.Now.Subtract(x.BirthDay).Days)/365 < maxAge && (DateTime.Now.Subtract(x.BirthDay).Days)/365 > minAge && x.Account.Visibility == true
                 select new User()
                 {
                     Id = x.Id,
                     Username = x.Username,
                     FirstName = x.FirstName,
                     LastName = x.LastName,
                     BirthDay = x.BirthDay,
                     ProfilePhoto = x.ProfilePhoto,
                     PhoneNumber = x.PhoneNumber,
                     Account = new Account(){Email=x.Account.Email},
                     GenderId = x.GenderId,
                     Gender = new Gender(){ Id=x.GenderId, Type = x.Gender.Type },
                     UserRoles = (from u in _BaseDBContext.UserRoles where u.UserId == x.Id select new UserRole(){RoleId=u.RoleId}).ToList(),
                     CreatedActivities = (from ca in _BaseDBContext.Activities
                                          where ca.OwnerUserId == x.Id select ca).ToList(),
                     WishedActivities = (from w in _BaseDBContext.WishedActivites where w.UserId == x.Id select w).ToList(),
                     //UserActivityTimeTables = (from t in _BaseDBContext.UserActivityTimeTables where t.UserId == x.Id select t).ToList()
                     RecordedActivities = x.RecordedActivities
                 }
                ).ToList();
                return users;
    }

    public User UpdateUser(string UserName, UpdateUserDTO userDTO)
    {
        User user = _BaseDBContext.Users.FirstOrDefault(x=>x.Username == UserName);
        user.Username = userDTO.Username;
        user.FirstName = userDTO.FirstName;
        user.LastName = userDTO.LastName;
        user.BirthDay = userDTO.BirthDate;
        user.ProfilePhoto = userDTO.ProfilePhoto;
        user.PhoneNumber = userDTO.PhoneNumber;
        user.GenderId = userDTO.GenderId;
        _BaseDBContext.SaveChanges();
        return GetUserByUsername(userDTO.Username);
    }
}