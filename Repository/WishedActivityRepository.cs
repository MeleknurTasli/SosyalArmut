public class WishedActivityRepository : IWishedActivityRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public WishedActivityRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public async Task<WishedActivity> CreateWishedActivity(WishedActivity wishedActivity)
    {
        Activity _Activity = _BaseDBContext.Activities.Where(x => x.Id == wishedActivity.ActivityId).FirstOrDefault();
        User _User = _BaseDBContext.Users.Where(x => x.Id == wishedActivity.UserId).FirstOrDefault();
        if (_Activity != null && _User != null)
        {
            _BaseDBContext.WishedActivites.Add(wishedActivity);
            _BaseDBContext.SaveChanges();
            return wishedActivity;
        }
        return null;
    }

    public async Task DeleteWishedActivity(string UserName, int? ActivityId)
    {
        WishedActivity wishedActivity = await GetWishedActivityByUsernameAndActivityId(UserName, ActivityId);
        if (wishedActivity != null)
        {
            _BaseDBContext.WishedActivites.Remove(wishedActivity);
            await _BaseDBContext.SaveChangesAsync();
             //sor hangisi
            /*
            User user = _BaseDBContext.Users.FirstOrDefault(x=>x.Username == UserName);
            wishedActivity = user.WishedActivities.FirstOrDefault(x=>x.ActivityId == ActivityId);
            user.WishedActivities.Remove(wishedActivity);
            await _BaseDBContext.SaveChangesAsync();
            */
        }
    }

    public async Task<IEnumerable<WishedActivity>> GetAllWishedActivities()
    {
        return await _BaseDBContext.WishedActivites.ToListAsync();
    }

    public async Task<IEnumerable<WishedActivity>> GetWishedActivitiesByActivityId(int ActivityId)
    {
        return await _BaseDBContext.WishedActivites
                     .Where(x => x.ActivityId == ActivityId && x.Activity.Visibility == true).ToListAsync();
    }

    public async Task<IEnumerable<WishedActivity>> GetWishedActivitiesByUserName(string Username)
    {
        return await _BaseDBContext.WishedActivites.
                     Where(x=>x.User.Username== Username && x.Activity.Visibility == true).ToListAsync();
    }

    public async Task<WishedActivity> GetWishedActivityById(int Id)
    {
        var wishedActivity = await _BaseDBContext.WishedActivites.
                              Where(x => x.Id == Id).
                              FirstOrDefaultAsync();
        return wishedActivity;
    }

    public async Task<WishedActivity> GetWishedActivityByUsernameAndActivityId(string Username, int? ActivityId)
    {
        var wishedActivity = await _BaseDBContext.WishedActivites.
                             Where(x => x.User.Username == Username && x.ActivityId == ActivityId).
                             FirstOrDefaultAsync();
        return wishedActivity;
    }

    public bool IsExistsinUserActivityTimeTable(WishedActivity wishedActivity)
    {
        User? user =  _BaseDBContext.Users.Include(x=>x.RecordedActivities).FirstOrDefault(x=>x.Id == wishedActivity.UserId);
        List<ActivityTimeTable>? activityTimeTables =  _BaseDBContext.ActivityTimeTables.Where(x=>x.ActivityId == wishedActivity.ActivityId).ToList();
        foreach (ActivityTimeTable item in activityTimeTables)
        {
            if (user.RecordedActivities.Contains(item))
            {
                return true;
            }
        }
        return false;
        
        /*
        var useractivitytimetable =_BaseDBContext.UserActivityTimeTables.FirstOrDefault(x=>x.UserId == wishedActivity.UserId
                                              && x.ActivityTimeTable.ActivityId == wishedActivity.ActivityId);
        if(useractivitytimetable != null)
        {
            return true;
        }
        return false;
        */
    }

    public string getUserNameById(int? Id)
    {
        return _BaseDBContext.Users.FirstOrDefault(x => x.Id == Id).Username;
    }

    public bool IsExistsInActivities(int Id)
    {
        return  _BaseDBContext.Activities.Any(x=>x.Id == Id);
    }
}