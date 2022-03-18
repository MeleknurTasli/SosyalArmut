public class UserActivityTimeTableService : IUserActivityTimeTableService
{
    private readonly IUserActivityTimeTableRepository _userActivityTimeTableRepository;
    public UserActivityTimeTableService(IUserActivityTimeTableRepository userActivityTimeTableRepository)
    {
        _userActivityTimeTableRepository = userActivityTimeTableRepository;
    }

    public Task<CreateUserActivityTimeTableDTO> CreateUserActivityTimeTable(CreateUserActivityTimeTableDTO createUserActivityTimeTableDTO)
    {
        throw new NotImplementedException();
    }

    public Task DeleteUserActivityTimeTable(string Username, int timeTableId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserActivityTimeTableDTO>> GetPastRecordsByUserName(string Username)
    {
        throw new NotImplementedException();
    }

    public Task<UserActivityTimeTableDTO> GetRecordByUserNameAndTimeTableId(string Username, int timeTableId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserActivityTimeTableDTO>> GetUpcomingRecordsByUserName(string Username)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDTO>> GetUsersWhoAttendedToActivity(int ActivityId)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserDTO>> GetUsersWhoWillAttendToActivity(int ActivityId)
    {
        throw new NotImplementedException();
    }

    public Task<UserActivityTimeTableDTO> UpdateUserActivityTimeTable(string Username, int timeTableId)
    {
        throw new NotImplementedException();
    }
}