namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class ActivityController : ControllerBase
{
    private readonly IActivityService _ActivityService;
    public ActivityController(IActivityService ActivityService)
    {
        _ActivityService = ActivityService;
    }

    [HttpPut("{Id}")]
    public async Task<ActionResult> ChangeVisibilityOfActivity(int Id)
    {
        return await _ActivityService.ChangeVisibilityOfActivity(Id);
    }

    [HttpPut]
    public async Task<ActionResult<UpdateActivityDTO>> UpdateActivity(UpdateActivityDTO Activity)
    {
        return await _ActivityService.UpdateActivity(Activity);
    }

    [HttpPost]
    public async Task<ActionResult<ActivityDTO>> CreateActivity(CreateActivityDTO Activity)
    {
        return await _ActivityService.CreateActivity(Activity);
    }

    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteActivity(int Id)  //en son test et
    {
        return await _ActivityService.DeleteActivity(Id);
    }

    [HttpGet("CategoryName")]
    public async Task<IEnumerable<ActivityDTO>> GetActivitiesByCategoryNameOrderByCreatedTime(string CategoryName)
    {
        return await _ActivityService.GetActivitiesByCategoryNameOrderByCreatedTime(CategoryName);
    }

    [Route("[action]")]
    [HttpGet]  //https://localhost:7293/Activity/GetActivitiesByNeighbourhood
    public async Task<IEnumerable<ActivityDTO>> GetActivitiesByNeighbourhood(Neighbourhood Neighbourhood)
    {
        return await _ActivityService.GetActivitiesByNeighbourhood(Neighbourhood);
    }

    [HttpGet("Username")]
    public async Task<IEnumerable<ActivityDTO>> GetActivitiesByOwnerUsername(string Username)
    {
        return await _ActivityService.GetActivitiesByOwnerUsername(Username);
    }

    [HttpGet("Price")]//Date?FirstDate=2022-03-04T18:25:43.511Z&LastDate=2022-03-06T18:25:43.511
    public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetActivitiesByPriceLimits(string minPrice, string maxPrice)
    {
        return await _ActivityService.GetActivitiesByPriceLimits(minPrice, maxPrice);
    }

    [HttpGet("SubCategoryName")]
    public async Task<IEnumerable<ActivityDTO>> GetActivitiesBySubCategoryNameOrderByCreatedTime(string SubCategoryName)
    {
        return await _ActivityService.GetActivitiesBySubCategoryNameOrderByCreatedTime(SubCategoryName);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<ActivityDTO>> GetActivityById(int Id)
    {
        return await _ActivityService.GetActivityById(Id);
    }

    [HttpGet("Name")]
    public async Task<ActionResult<ActivityDTO>> GetActivityByName(string Name)
    {
        return await _ActivityService.GetActivityByName(Name);
    }

    [HttpGet]
    public async Task<IEnumerable<ActivityDTO>> GetAllActivitiesOrderByCreatedTime()
    {
        return await _ActivityService.GetAllActivitiesOrderByCreatedTime();
    }

    [HttpGet("minPoint")] 
    public async Task<ActionResult<IEnumerable<ActivityDTO>>> GetAllActivitiesByPointLimit(string minPoint)
    {
        return await _ActivityService.GetAllActivitiesByPointLimit(minPoint);
    }
}