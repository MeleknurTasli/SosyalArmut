public interface IUserActivityTimeTableService
{
    public Task<CreateUserActivityTimeTableDTO> CreateUserActivityTimeTable(CreateUserActivityTimeTableDTO createUserActivityTimeTableDTO);
    public Task DeleteUserActivityTimeTable(string Username, int timeTableId);
    public Task<UserActivityTimeTableDTO> UpdateUserActivityTimeTable(string Username, int timeTableId); //Isupcoming için
    public Task<UserActivityTimeTableDTO> GetRecordByUserNameAndTimeTableId(string Username, int timeTableId);
    public Task<IEnumerable<UserActivityTimeTableDTO>> GetUpcomingRecordsByUserName(string Username);
    public Task<IEnumerable<UserActivityTimeTableDTO>> GetPastRecordsByUserName(string Username);
    public Task<IEnumerable<UserDTO>> GetUsersWhoAttendedToActivity(int ActivityId);
    public Task<IEnumerable<UserDTO>> GetUsersWhoWillAttendToActivity(int ActivityId);


    //public UserActivityTimeTable GetUserActivityTimeTableById(int Id);
}
