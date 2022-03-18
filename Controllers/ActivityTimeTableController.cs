[ApiController]
[Route("[controller]")]
public class ActivityTimeTableController : ControllerBase
{
    private readonly IActivityTimeTableService _ActivityTimeTableService;
    public ActivityTimeTableController(IActivityTimeTableService ActivityTimeTableService)
    {
        _ActivityTimeTableService = ActivityTimeTableService;
    }

    [HttpPost] 
    public async Task<ActionResult<ActivityTimeTable>> CreateActivityTimeTable(CreateUpdateActivityTimeTableDTO createActivityTimeTableDTO)
    {
        return await _ActivityTimeTableService.CreateActivityTimeTable(createActivityTimeTableDTO);
    }

    [HttpDelete("{activityTimeTableId}")]//recordan sonra da dene
    public async Task DeleteActivityTimeTable(int activityTimeTableId)
    {
        await _ActivityTimeTableService.DeleteActivityTimeTable(activityTimeTableId);
    }

    [HttpGet("timeTableId")]
    public async Task<ActivityTimeTable> GetActivityTimeTableById(int timeTableId)
    {
        return await _ActivityTimeTableService.GetActivityTimeTableById(timeTableId);
    }

    [HttpGet("ActivityIdForAll")]
    public async Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityId(int ActivityIdForAll)
    {
        return await _ActivityTimeTableService.GetActivityTimeTablesByActivityId(ActivityIdForAll);
    }

    [HttpGet("ActivityId")]
    public async Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesByActivityIdNotFullAndIsUpcoming(int ActivityId)
    {
        return await _ActivityTimeTableService.GetActivityTimeTablesByActivityIdNotFullAndIsUpcoming(ActivityId);
    }

    [HttpGet("SubcategoryName")]
    public async Task<IEnumerable<ActivityTimeTable>> GetActivityTimeTablesBySubCategoryNameNotFullAndIsUpcoming(string SubcategoryName)
    {
        return await _ActivityTimeTableService.GetActivityTimeTablesBySubCategoryNameNotFullAndIsUpcoming(SubcategoryName);
    }

    [HttpGet]
    public async Task<IEnumerable<ActivityTimeTable>> GetAllActivityTimeTables()
    {
        return await _ActivityTimeTableService.GetAllActivityTimeTables();
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult<ActivityTimeTable>> UpdateActivityTimeTable(int Id, CreateUpdateActivityTimeTableDTO updateActivityTimeTableDTO)
    {
        return await _ActivityTimeTableService.UpdateActivityTimeTable(Id, updateActivityTimeTableDTO);
    }

    [HttpPut("timeTableId")]
    public async Task<ActionResult<ActivityTimeTable>> UpdateIsUpcoming(int timeTableId)
    {
        return await _ActivityTimeTableService.UpdateIsUpcoming(timeTableId);
    }









    [HttpDelete("DeleteRecord")]
    public async Task DeleteUserFromActivityTimeTable(string Username, int timeTableId)
    {
        await _ActivityTimeTableService.DeleteUserFromActivityTimeTable(Username, timeTableId);
    }

    [HttpGet("UsernameForPastRecords")]
    public async Task<ActionResult<IEnumerable<ActivityTimeTable>>> GetPastRecordsByUserName(string UsernameForPastRecords)
    {
        return await _ActivityTimeTableService.GetPastRecordsByUserName(UsernameForPastRecords);
    }

    [HttpGet("Record")]
    public async Task<ActionResult<ActivityTimeTable>> GetRecordByUserNameAndTimeTableId(string Username, int timeTableId)
    {
        return await _ActivityTimeTableService.GetRecordByUserNameAndTimeTableId(Username, timeTableId);
    }

    [HttpGet("UsernameForRecords")]
    public async Task<ActionResult<IEnumerable<ActivityTimeTable>>> GetUpcomingRecordsByUserName(string UsernameForRecords)
    {
        return await _ActivityTimeTableService.GetUpcomingRecordsByUserName(UsernameForRecords);
    }

    [HttpGet("ActivityIdForOldUsers")]
    public async Task<IEnumerable<IEnumerable<UserDTO>>> GetUsersWhoAttendedToActivity(int ActivityIdForOldUsers)
    {
        return await _ActivityTimeTableService.GetUsersWhoAttendedToActivity(ActivityIdForOldUsers);
    }

    [HttpGet("ActivityIdForUsers")]
    public async Task<IEnumerable<IEnumerable<UserDTO>>> GetUsersWhoWillAttendToActivity(int ActivityIdForUsers)
    {
        return await _ActivityTimeTableService.GetUsersWhoWillAttendToActivity(ActivityIdForUsers);
    }

    [HttpPost("Record")]  
    public async Task<ActionResult<ActivityTimeTable>> RecordUserToActivityTimeTable(string Username, int timeTableId)
    {
        return await _ActivityTimeTableService.RecordUserToActivityTimeTable(Username, timeTableId);
    }

    
}