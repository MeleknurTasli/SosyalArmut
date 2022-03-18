public interface IWishedActivityService
{
    public Task<ActionResult<WishedActivityDTO>> CreateWishedActivity(WishedActivity wishedActivity);
    public Task DeleteWishedActivity(string UserName, int ActivityId);
    public Task<ActionResult<WishedActivityDTO>> GetWishedActivityById(int Id);
    public Task<IEnumerable<WishedActivityDTO>> GetWishedActivitiesByUserName(string Username);
    public Task<IEnumerable<WishedActivityDTO>> GetWishedActivitiesByActivityId(int ActivityId);
    public Task<ActionResult<WishedActivityDTO>> GetWishedActivityByUsernameAndActivityId(string Username, int ActivityId);
    public Task<IEnumerable<WishedActivityDTO>> GetAllWishedActivities();

    
}