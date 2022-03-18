public interface IRankingService
{
    public Task<IEnumerable<RankingDTO>> GetAllRankings();
    public Task<ActionResult<RankingDTO>> CreateRanking(Ranking Ranking);
    public Task<ActionResult<RankingDTO>> UpdateRanking(UpdateRankingDTO Ranking); 
    public Task DeleteteRanking(int Id);
    public Task<IEnumerable<RankingDTO>> GetRankingsOfAnActivity(int ActivityId);
    public Task<ActionResult<RankingDTO>> GetRankingOfActivityByUsername(int ActivityId, string userName);
    public Task<IEnumerable<RankingDTO>> GetRankingsByUsername(string userName);
}