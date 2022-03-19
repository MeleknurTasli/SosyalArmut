namespace Armut.Model
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public int? Age { get; set; }
        public string? ProfilePhoto { get; set; }
        public string? PhoneNumber { get; set; }
        public int AccountId { get; set; }
        public virtual Account? Account { get; set; }

        public int GenderId { get; set; }
        public virtual Gender? Gender { get; set; }

        public virtual IEnumerable<UserRole>? UserRoles { get; set; }
        public virtual List<WishedActivity>? WishedActivities { get; set; }
        public virtual IEnumerable<Activity>? CreatedActivities { get; set; }
        public virtual IEnumerable<UserActivityTimeTable>? UserActivityTimeTables { get; set; }
        public virtual IEnumerable<Ranking>? GivenRankings { get; set; }

        public virtual List<ActivityTimeTable>? RecordedActivities { get; set; }
        public virtual List<Role>? Roles { get; set; }


        public User()
        {
            Roles = new List<Role>();
            //RecordedActivities = new List<ActivityTimeTable>();
        }

        

/*
        BaseDBContext _db;
        public User(CreateUserDTO _createUserDTO, BaseDBContext _db)
        {
            this._db = _db;
            Username = _createUserDTO.Username;
            FirstName = _createUserDTO.FirstName;
            LastName = _createUserDTO.LastName;
            Age = _createUserDTO.Age;
            ProfilePhoto = _createUserDTO.ProfilePhoto;
            PhoneNumber = _createUserDTO.PhoneNumber;
            AccountId = _createUserDTO.AccountId;
            GenderId = _createUserDTO.GenderId;
            foreach (int roleId in _createUserDTO.RoleIds) 
            {
                _db.UserRoles.Add(new UserRole()
                {
                    UserId = Id,  
                    RoleId = roleId
                });
                _db.SaveChanges();
            } 
        }
*/

        public User(CreateUserDTO _createUserDTO)
        {
            Username = _createUserDTO.Username;
            FirstName = _createUserDTO.FirstName;
            LastName = _createUserDTO.LastName;
            Age = _createUserDTO.Age;
            ProfilePhoto = _createUserDTO.ProfilePhoto;
            PhoneNumber = _createUserDTO.PhoneNumber;
            AccountId = _createUserDTO.AccountId;
            GenderId = _createUserDTO.GenderId; 
        }
    }
}