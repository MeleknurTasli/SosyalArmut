public interface IUserActivityTimeTableRepository
{
    public UserActivityTimeTable CreateUserActivityTimeTable(CreateUserActivityTimeTableDTO createUserActivityTimeTableDTO);
    public void DeleteUserActivityTimeTable(string Username, int timeTableId);
    public UserActivityTimeTable UpdateUserActivityTimeTable(string Username, int timeTableId); //Isupcoming için
    public UserActivityTimeTable GetRecordByUserNameAndTimeTableId(string Username, int timeTableId);
    public IEnumerable<UserActivityTimeTable> GetUpcomingRecordsByUserName(string Username);
    public IEnumerable<UserActivityTimeTable> GetPastRecordsByUserName(string Username);
    public IEnumerable<User> GetUsersWhoAttendedToActivity(int ActivityId);
    public IEnumerable<User> GetUsersWhoWillAttendToActivity(int ActivityId);


      //public UserActivityTimeTable GetUserActivityTimeTableById(int Id);
}