namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class WishedActivityController : ControllerBase
{
    private readonly IWishedActivityService _WishedActivityService;
    public WishedActivityController(IWishedActivityService WishedActivityService)
    {
        _WishedActivityService = WishedActivityService;
    }

    [HttpPost]
    public async Task<ActionResult<WishedActivityDTO>> CreateWishedActivity(WishedActivity wishedActivity)
    {
        return await _WishedActivityService.CreateWishedActivity(wishedActivity);
    }

    [HttpDelete("deleteinfo")]  //https://localhost:7293/WishedActivity/deleteinfo?Username=melek1&ActivityId=7
    public async Task DeleteWishedActivity(string UserName, int ActivityId)
    {
        await _WishedActivityService.DeleteWishedActivity(UserName, ActivityId);
    }

    [HttpGet]
    public async Task<IEnumerable<WishedActivityDTO>> GetAllWishedActivities()
    {
        return await _WishedActivityService.GetAllWishedActivities();
    }

    [HttpGet("ActivityId")]
    public async Task<IEnumerable<WishedActivityDTO>> GetWishedActivitiesByActivityId(int ActivityId)
    {
        return await _WishedActivityService.GetWishedActivitiesByActivityId(ActivityId);
    }

    [HttpGet("Username")]
    public async Task<IEnumerable<WishedActivityDTO>> GetWishedActivitiesByUserName(string Username)
    {
        return await _WishedActivityService.GetWishedActivitiesByUserName(Username);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<WishedActivityDTO>> GetWishedActivityById(int Id)
    {
        return await _WishedActivityService.GetWishedActivityById(Id);
    }

    [HttpGet("WishedActivity")]  //https://localhost:7293/WishedActivity/WishedActivity?Username=melek1&ActivityId=7
    public async Task<ActionResult<WishedActivityDTO>> GetWishedActivityByUsernameAndActivityId(string Username, int ActivityId)
    {
        return await _WishedActivityService.GetWishedActivityByUsernameAndActivityId(Username, ActivityId);
    }
}