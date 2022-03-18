public interface IActivityTimeTableService
{
    public Task<ActionResult<ActivityTimeTable>> CreateActivityTimeTable(CreateUpdateActivityTimeTableDTO createActivityTimeTableDTO);
    public Task<ActionResult<ActivityTimeTable>> UpdateActivityTimeTable(int Id, CreateUpdateActivityTimeTableDTO updateActivityTimeTableDTO);
    public Task DeleteActivityTimeTable(int activityTimeTableId);
    public Task<IEnumerable<ActivityTimeTable>> GetAllActivityTimeTables();
    public Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityId(int ActivityId);
    public Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesBySubCategoryNameNotFullAndIsUpcoming(string name);
    public Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityIdNotFullAndIsUpcoming(int ActivityId);
    public Task<ActivityTimeTable> GetActivityTimeTableById(int timeTableId);


    
    
    public Task<ActionResult<ActivityTimeTable>> RecordUserToActivityTimeTable(string Username, int timeTableId);
    public Task DeleteUserFromActivityTimeTable(string Username, int timeTableId);
    public Task<ActionResult<ActivityTimeTable>> UpdateIsUpcoming(int timeTableId); //Isupcoming için //trigger
    public Task<ActionResult<ActivityTimeTable>> GetRecordByUserNameAndTimeTableId(string Username, int timeTableId);
    public Task<ActionResult<IEnumerable<ActivityTimeTable>>> GetUpcomingRecordsByUserName(string Username);
    public Task<ActionResult<IEnumerable<ActivityTimeTable>>> GetPastRecordsByUserName(string Username);
    public Task<IEnumerable<IEnumerable<UserDTO>>> GetUsersWhoAttendedToActivity(int ActivityId);
    public Task<IEnumerable<IEnumerable<UserDTO>>> GetUsersWhoWillAttendToActivity(int ActivityId);
}