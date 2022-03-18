public class ActivityTimeTableRepository : IActivityTimeTableRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public ActivityTimeTableRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public async Task<ActivityTimeTable> GetActivityTimeTableById(int timeTableId)
    {
        return await _BaseDBContext.ActivityTimeTables.FirstOrDefaultAsync(x => x.Id == timeTableId);
    }

    public async Task<ActivityTimeTable> CreateActivityTimeTable(CreateUpdateActivityTimeTableDTO createActivityTimeTableDTO)
    {
        ActivityTimeTable activityTimeTable = new ActivityTimeTable(createActivityTimeTableDTO);
        await _BaseDBContext.ActivityTimeTables.AddAsync(activityTimeTable);
        await _BaseDBContext.SaveChangesAsync();
        return activityTimeTable;
        
    }

    public async Task DeleteActivityTimeTable(int activityTimeTableId)
    {
        ActivityTimeTable? activityTimeTable = await _BaseDBContext.ActivityTimeTables.Include(x=>x.AllAttendantUsers)
                                    .FirstOrDefaultAsync(x=>x.Id == activityTimeTableId && x.IsUpComing == true);
        //Activity? activity = await _BaseDBContext.Activities.FirstOrDefaultAsync(x=>x.Id == activityTimeTable.ActivityId);
        //User user = await _BaseDBContext.Users.Include(x=>x.RecordedActivities).FirstOrDefaultAsync(x=>x.Id == activity.OwnerUserId);
        if (activityTimeTable != null)
        {
            List<User>? users = activityTimeTable.AllAttendantUsers;
            int number = activityTimeTable.AllAttendantUsers.Count;
            for (int i = 0; i < number; i++)  //foreach (User user in users)
            {
                User _user = await _BaseDBContext.Users.Include(x=>x.RecordedActivities).FirstOrDefaultAsync(x=>x.Id == users[i].Id);
                _user.RecordedActivities.Remove(activityTimeTable);
                await _BaseDBContext.SaveChangesAsync();
            }
        }
        _BaseDBContext.ActivityTimeTables.Remove(activityTimeTable);
        await _BaseDBContext.SaveChangesAsync();

        /*
        if (activity != null && activityTimeTable != null)
        {
            if (activityTimeTable.AllAttendantUsers.Contains(activityTimeTable))
            {
                activity.OwnerUser.RecordedActivities.Remove(activityTimeTable);
                await _BaseDBContext.SaveChangesAsync();
            }
            _BaseDBContext.ActivityTimeTables.Remove(activityTimeTable);
            await _BaseDBContext.SaveChangesAsync();
        }
        */
    }

    public async Task<ActivityTimeTable> UpdateActivityTimeTable(int Id, CreateUpdateActivityTimeTableDTO updateActivityTimeTableDTO)
    {
        ActivityTimeTable activityTimeTable = await _BaseDBContext.ActivityTimeTables.FirstOrDefaultAsync(x=>x.Id == Id);
        activityTimeTable.ActivityId = updateActivityTimeTableDTO.ActivityId;
        activityTimeTable.StartDate = updateActivityTimeTableDTO.StartDate;
        activityTimeTable.EndDate = updateActivityTimeTableDTO.EndDate;
        activityTimeTable.Quota = updateActivityTimeTableDTO.Quota;
         activityTimeTable.Visibility = updateActivityTimeTableDTO.Visibility;
        await _BaseDBContext.SaveChangesAsync();
        return activityTimeTable;
    }

    public async Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityId(int ActivityId)
    {
        return await  _BaseDBContext.ActivityTimeTables.Where(x=>x.ActivityId == ActivityId).ToListAsync();
    }

    public async Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityIdNotFullAndIsUpcoming(int ActivityId)
    {
        return await _BaseDBContext.ActivityTimeTables.Where(x=>x.ActivityId == ActivityId 
                                                && x.IsUpComing == true && x.Quota > x.NumberOfAttendants).ToListAsync();
    }

    public async Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesBySubCategoryNameNotFullAndIsUpcoming(string name)
    {
        return await _BaseDBContext.ActivityTimeTables.Where(x=>x.Activity.SubCategory.Name == name
                                                && x.IsUpComing == true && x.Quota > x.NumberOfAttendants).ToListAsync();
    }

    public async Task<IEnumerable<ActivityTimeTable>> GetAllActivityTimeTables()
    {
        return await _BaseDBContext.ActivityTimeTables.ToListAsync();
    }









    public void DeleteUserFromActivityTimeTable(string Username, int timeTableId)
    {
        ActivityTimeTable? activityTimeTable = _BaseDBContext.ActivityTimeTables.Include(x=>x.AllAttendantUsers).
                                Where(x => x.Id == timeTableId).FirstOrDefault();
        User? user = _BaseDBContext.Users.Include(x=>x.RecordedActivities).
                                Where(x => x.Username == Username).FirstOrDefault();
        Activity? activity = _BaseDBContext.Activities.Where(x => x.Id == activityTimeTable.ActivityId).FirstOrDefault();
        if(activityTimeTable != null && user != null && activity != null)
        {
            user.RecordedActivities.Remove(activityTimeTable);
            //_BaseDBContext.SaveChanges();
            activityTimeTable.AllAttendantUsers.Remove(user);
            _BaseDBContext.SaveChanges();
            activityTimeTable.NumberOfAttendants -= 1;
            _BaseDBContext.SaveChanges();
        }
    }

    public IEnumerable<ActivityTimeTable> GetPastRecordsByUserName(string Username)
    {
        User user = _BaseDBContext.Users.Include(x=>x.RecordedActivities).Where(x => x.Username == Username).FirstOrDefault();
        return user.RecordedActivities.Where(x=>x.IsUpComing == false).ToList();
    }

    public ActivityTimeTable GetRecordByUserNameAndTimeTableId(string Username, int timeTableId)
    {
        User user = _BaseDBContext.Users.Include(x=>x.RecordedActivities).Where(x => x.Username == Username).FirstOrDefault();
        ActivityTimeTable ActivityTimeTable = user.RecordedActivities.Where(x=>x.Id == timeTableId).FirstOrDefault();
        return ActivityTimeTable;
    }

    public IEnumerable<ActivityTimeTable> GetUpcomingRecordsByUserName(string Username)
    {
        User user = _BaseDBContext.Users.Include(x=>x.RecordedActivities).Where(x => x.Username == Username).FirstOrDefault();
        return user.RecordedActivities.Where(x=>x.IsUpComing == true).ToList();
    }

    public IEnumerable<IEnumerable<User>> GetUsersWhoAttendedToActivity(int ActivityId)
    {
        //Activity? activity = _BaseDBContext.Activities.Where(x => x.Id == ActivityId).FirstOrDefault();
        List<List<User>> AllUsers = new List<List<User>>();
        List<User> Users = new List<User>();
        foreach(ActivityTimeTable item in _BaseDBContext.ActivityTimeTables.Include(x=>x.AllAttendantUsers).Where(x=>x.ActivityId == ActivityId && x.IsUpComing == false).ToList())
        {
            foreach(User user in item.AllAttendantUsers)
            {
                Users.Add(user);
            }
            AllUsers.Add(Users);
        }
        return AllUsers;
    }

    public IEnumerable<IEnumerable<User>> GetUsersWhoWillAttendToActivity(int ActivityId)
    {
        //Activity? activity = _BaseDBContext.Activities.Where(x => x.Id == ActivityId).FirstOrDefault();
        List<List<User>> AllUsers = new List<List<User>>();
        List<User> Users = new List<User>();
        var liste = _BaseDBContext.ActivityTimeTables.Include(x=>x.AllAttendantUsers).Where(x=>x.ActivityId == ActivityId && x.IsUpComing == true).ToList();
        foreach(ActivityTimeTable item in liste)
        {
            foreach(User user in item.AllAttendantUsers)
            {
                Users.Add(user);
            }
            AllUsers.Add(Users);
        }
        return AllUsers;
    }

    public ActionResult<ActivityTimeTable> RecordUserToActivityTimeTable(string Username, int timeTableId)
    {
        ActivityTimeTable? activityTimeTable = _BaseDBContext.ActivityTimeTables.Where(x => x.Id == timeTableId).FirstOrDefault();
        User? user = _BaseDBContext.Users. Where(x => x.Username == Username).FirstOrDefault();
        Activity? activity = _BaseDBContext.Activities.Where(x => x.Id == activityTimeTable.ActivityId).FirstOrDefault();

        activityTimeTable.AllAttendantUsers = new List<User>();
        user.RecordedActivities = new List<ActivityTimeTable>();

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
            activityTimeTable.AllAttendantUsers.Add(user);
            user.RecordedActivities.Add(activityTimeTable);
            _BaseDBContext.SaveChanges();
            return activityTimeTable;
        }
        return null;
    }

    public ActivityTimeTable UpdateIsUpcoming(int timeTableId)
    {
        ActivityTimeTable? ActivityTimeTable = _BaseDBContext.ActivityTimeTables.Where(x => x.Id == timeTableId).FirstOrDefault();
        ActivityTimeTable.IsUpComing = false;
        _BaseDBContext.SaveChanges();
        return ActivityTimeTable;
    }


   public async Task<bool>  IsUsernameExist(string username)
   {
        if (await _BaseDBContext.Users.FirstOrDefaultAsync(x=>x.Username == username) != null)
        {
            return true;
        }
        return false;
   }




    public async Task<bool> IsActivityExist(int activityId)
    {
        if (await _BaseDBContext.Activities.FirstOrDefaultAsync(x=>x.Id == activityId) != null)
        {
            return true;
        }
        return false;
    }
}