public interface IActivityService
{
    public Task<IEnumerable<ActivityDTO>> GetAllActivitiesOrderByCreatedTime();
    public Task<IEnumerable<ActivityDTO>> GetActivitiesBySubCategoryNameOrderByCreatedTime(string SubCategoryName);
    public Task<IEnumerable<ActivityDTO>> GetActivitiesByCategoryNameOrderByCreatedTime(string CategoryName);
    public Task<ActionResult<ActivityDTO>> GetActivityByName(string Name);
    public Task<ActionResult<ActivityDTO>> GetActivityById(int Id);
    public Task<ActionResult<IEnumerable<ActivityDTO>>> GetActivitiesByPriceLimits(string minPrice, string maxPrice);
    public Task<IEnumerable<ActivityDTO>> GetActivitiesByOwnerUsername(string Username);
    public Task<IEnumerable<ActivityDTO>> GetActivitiesByNeighbourhood(Neighbourhood Neighbourhood);
    public Task<ActionResult<ActivityDTO>> CreateActivity(CreateActivityDTO Activity);
    public Task<ActionResult<UpdateActivityDTO>> UpdateActivity(UpdateActivityDTO Activity);
    public Task<ActionResult> DeleteActivity(int Id); 
    public Task<ActionResult> ChangeVisibilityOfActivity(int Id);
    public Task<ActionResult<IEnumerable<ActivityDTO>>> GetAllActivitiesByPointLimit(string minValue);

}