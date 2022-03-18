public interface IActivityTimeTableRepository
{
    public Task<ActivityTimeTable> CreateActivityTimeTable(CreateUpdateActivityTimeTableDTO createActivityTimeTableDTO);
    public Task<ActivityTimeTable> UpdateActivityTimeTable(int Id, CreateUpdateActivityTimeTableDTO updateActivityTimeTableDTO);
    public Task DeleteActivityTimeTable(int activityTimeTableId);
    public Task<IEnumerable<ActivityTimeTable>> GetAllActivityTimeTables();
    public Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityId(int ActivityId);
    public Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesBySubCategoryNameNotFullAndIsUpcoming(string name);
    public Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityIdNotFullAndIsUpcoming(int ActivityId);
    public Task<ActivityTimeTable> GetActivityTimeTableById(int timeTableId);


    
    
    public ActionResult<ActivityTimeTable> RecordUserToActivityTimeTable(string Username, int timeTableId);
    public void DeleteUserFromActivityTimeTable(string Username, int timeTableId);
    public ActivityTimeTable UpdateIsUpcoming(int timeTableId); //Isupcoming için
    public ActivityTimeTable GetRecordByUserNameAndTimeTableId(string Username, int timeTableId);
    public IEnumerable<ActivityTimeTable> GetUpcomingRecordsByUserName(string Username);
    public IEnumerable<ActivityTimeTable> GetPastRecordsByUserName(string Username);
    public IEnumerable<IEnumerable<User>> GetUsersWhoAttendedToActivity(int ActivityId);
    public IEnumerable<IEnumerable<User>> GetUsersWhoWillAttendToActivity(int ActivityId);


     public Task<bool> IsActivityExist(int activityId);
     public Task<bool>  IsUsernameExist(string username);

}