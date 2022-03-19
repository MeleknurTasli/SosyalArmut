public interface IActivityRepository
{
    public Task<IEnumerable<Activity>> GetAllActivitiesOrderByCreatedTime();
    public Task<IEnumerable<Activity>> GetActivitiesBySubCategoryNameOrderByCreatedTime(string SubCategoryName);
    public Task<IEnumerable<Activity>> GetActivitiesByCategoryNameOrderByCreatedTime(string CategoryName);
    public Task<Activity> GetActivityByName(string Name);
    public Task<Activity> GetActivityById(int Id);
    public Task<IEnumerable<Activity>> GetActivitiesByPriceLimits(double minPrice, double maxPrice);
    public Task<IEnumerable<Activity>> GetActivitiesByOwnerUsername(string Username);
    public Task<IEnumerable<Activity>> GetActivitiesByNeighbourhood(Neighbourhood Neighbourhood);
    public Task<Activity> CreateActivity(CreateActivityDTO Activity); 
    public Task<Activity> UpdateActivity(UpdateActivityDTO Activity);
    public Task<ActionResult> DeleteActivity(int Id); 
    public Task<ActionResult> ChangeVisibilityOfActivity(int Id);
    public Task <IEnumerable<Activity>> GetAllActivitiesByPointLimit(double minValue);
    public bool IsAllGivenIDsCorrect(int? AddressId, int? SubCategoryId, int userId);

}