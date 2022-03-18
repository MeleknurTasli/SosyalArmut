namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class RankingController : ControllerBase
{
    private readonly IRankingService _RankingService;
    public RankingController(IRankingService RankingService)
    {
        _RankingService = RankingService;
    }

    [HttpGet]
    public async Task<IEnumerable<RankingDTO>> GetAllRankings()
    {
        return await _RankingService.GetAllRankings();
    }

    [HttpPost]
    public async Task<ActionResult<RankingDTO>> CreateRanking(Ranking Ranking)
    {
        return await _RankingService.CreateRanking(Ranking);
    }

    [HttpPut]
    public async Task<ActionResult<RankingDTO>> UpdateRanking(UpdateRankingDTO Ranking)
    {
        return await _RankingService.UpdateRanking(Ranking);
    }

    [HttpDelete("{Id}")]
    public async Task DeleteteRanking(int Id)
    {
        await _RankingService.DeleteteRanking(Id);
    }

    [HttpGet("ActivityId")]
    public async Task<IEnumerable<RankingDTO>> GetRankingsOfAnActivityById(int ActivityId)
    {
        return await _RankingService.GetRankingsOfAnActivity(ActivityId);
    }

    [HttpGet("Activity")]
    public async Task<ActionResult<RankingDTO>> GetRankingOfActivityByUsername(int ActivityId, string userName)
    {
        return await _RankingService.GetRankingOfActivityByUsername(ActivityId, userName);
    }

    [HttpGet("Username")]
    public async Task<IEnumerable<RankingDTO>> GetRankingsByUsername(string userName)
    {
        return await _RankingService.GetRankingsByUsername(userName);
    }
}