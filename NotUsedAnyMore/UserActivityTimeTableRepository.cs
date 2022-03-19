public class UserActivityTimeTableRepository : IUserActivityTimeTableRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public UserActivityTimeTableRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public UserActivityTimeTable CreateUserActivityTimeTable(CreateUserActivityTimeTableDTO createUserActivityTimeTableDTO)
    {
        ActivityTimeTable? activityTimeTable = _BaseDBContext.ActivityTimeTables.
                                Where(x => x.Id == createUserActivityTimeTableDTO.ActivityTimeTableId).FirstOrDefault();
        User? user = _BaseDBContext.Users.
                                Where(x => x.Id == createUserActivityTimeTableDTO.UserId).FirstOrDefault();
        Activity? activity = _BaseDBContext.Activities.
                                Where(x => x.Id == createUserActivityTimeTableDTO.ActivityTimeTable.ActivityId).FirstOrDefault();

        if (activityTimeTable != null && user != null && activity != null && activityTimeTable.Quota != 0)
        {
            if (activityTimeTable.NumberOfAttendants == null && activityTimeTable.NumberOfAttendants == 0)
            {
                activityTimeTable.NumberOfAttendants = 1;
                _BaseDBContext.SaveChanges();
            }
            else if(activityTimeTable.NumberOfAttendants < activityTimeTable.Quota)
            {
                activityTimeTable.NumberOfAttendants += 1;
                _BaseDBContext.SaveChanges();
            }
            else 
            {
                return null;
            }
            UserActivityTimeTable UserActivityTimeTable = new UserActivityTimeTable(createUserActivityTimeTableDTO);
            _BaseDBContext.UserActivityTimeTables.Add(UserActivityTimeTable);
            _BaseDBContext.SaveChanges();
            return UserActivityTimeTable;
        }
        return null;
    }

    public void DeleteUserActivityTimeTable(string Username, int timeTableId)
    {
        UserActivityTimeTable userActivityTimeTable = _BaseDBContext.UserActivityTimeTables
                               .Where(x=>x.ActivityTimeTableId == timeTableId && x.User.Username == Username).FirstOrDefault();
        ActivityTimeTable? activityTimeTable = _BaseDBContext.ActivityTimeTables.
                                Where(x => x.Id == userActivityTimeTable.ActivityTimeTableId).FirstOrDefault();
        User? user = _BaseDBContext.Users.
                                Where(x => x.Id == userActivityTimeTable.UserId).FirstOrDefault();
        Activity? activity = _BaseDBContext.Activities.
                                Where(x => x.Id == userActivityTimeTable.ActivityTimeTable.ActivityId).FirstOrDefault();
        if(userActivityTimeTable != null && activityTimeTable != null && user != null && activity != null)
        {
            _BaseDBContext.UserActivityTimeTables.Remove(userActivityTimeTable);
            _BaseDBContext.SaveChanges();
            activityTimeTable.NumberOfAttendants -= 1;
            _BaseDBContext.SaveChanges();
        }
    }

    public IEnumerable<UserActivityTimeTable> GetPastRecordsByUserName(string Username)
    {
         return _BaseDBContext.UserActivityTimeTables
                               .Where(x=>x.IsUpComing == false && x.User.Username == Username).ToList();
    }

    public UserActivityTimeTable GetRecordByUserNameAndTimeTableId(string Username, int timeTableId)
    {
        UserActivityTimeTable userActivityTimeTable = _BaseDBContext.UserActivityTimeTables
                               .Where(x=>x.ActivityTimeTableId == timeTableId && x.User.Username == Username).FirstOrDefault();
        return userActivityTimeTable;
    }

    public IEnumerable<UserActivityTimeTable> GetUpcomingRecordsByUserName(string Username)
    {
          return _BaseDBContext.UserActivityTimeTables
                               .Where(x=>x.IsUpComing == true && x.User.Username == Username).ToList();
    }

    public IEnumerable<User> GetUsersWhoAttendedToActivity(int ActivityId)
    {
        return (from x in _BaseDBContext.UserActivityTimeTables
                where x.ActivityTimeTable.ActivityId == ActivityId && x.IsUpComing == false
                select new User()
                {
                    Username = x.User.Username,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    BirthDay = x.User.BirthDay,
                    ProfilePhoto = x.User.ProfilePhoto,
                    PhoneNumber = x.User.PhoneNumber,
                    Gender = new Gender(){Type = x.User.Gender.Type},
                    CreatedActivities = (from c in _BaseDBContext.Activities where x.UserId == c.OwnerUserId select c).ToList()
                }).ToList();
    }

    public IEnumerable<User> GetUsersWhoWillAttendToActivity(int ActivityId)
    {
         return (from x in _BaseDBContext.UserActivityTimeTables
                where x.ActivityTimeTable.ActivityId == ActivityId && x.IsUpComing == true
                select new User()
                {
                    Username = x.User.Username,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    BirthDay = x.User.BirthDay,
                    ProfilePhoto = x.User.ProfilePhoto,
                    PhoneNumber = x.User.PhoneNumber,
                    Gender = new Gender(){Type = x.User.Gender.Type},
                    CreatedActivities = (from c in _BaseDBContext.Activities where x.UserId == c.OwnerUserId select c).ToList()
                }).ToList();
    }

    public UserActivityTimeTable UpdateUserActivityTimeTable(string Username, int timeTableId)
    {
         UserActivityTimeTable? userActivityTimeTable = _BaseDBContext.UserActivityTimeTables
                               .Where(x=>x.ActivityTimeTableId == timeTableId && x.User.Username == Username).FirstOrDefault();
        if(userActivityTimeTable != null)
        {
            userActivityTimeTable.IsUpComing = false;
            _BaseDBContext.SaveChanges();
        }
        return userActivityTimeTable;
    }
}