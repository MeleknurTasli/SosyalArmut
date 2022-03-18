public interface IWishedActivityRepository   //bunda update olamaz yalnızca silme ve ekleme olur
{
    public Task<WishedActivity> CreateWishedActivity(WishedActivity wishedActivity); //sor userId
    public Task DeleteWishedActivity(string UserName, int? ActivityId);
    public Task<WishedActivity> GetWishedActivityById(int Id);
    public Task<IEnumerable<WishedActivity>> GetWishedActivitiesByUserName(string Username);
    public Task<IEnumerable<WishedActivity>> GetWishedActivitiesByActivityId(int ActivityId);
    public Task<WishedActivity> GetWishedActivityByUsernameAndActivityId(string Username, int? ActivityId);
    public Task<IEnumerable<WishedActivity>> GetAllWishedActivities();


    
    public bool IsExistsinUserActivityTimeTable(WishedActivity wishedActivity);
    public string getUserNameById(int? Id);
    public bool IsExistsInActivities(int Id);
}