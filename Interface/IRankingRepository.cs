public interface IRankingRepository
{
    public IEnumerable<Ranking> GetAllRankings();
    public Ranking CreateRanking(Ranking Ranking); 
    public Ranking UpdateRanking(UpdateRankingDTO Ranking); 
    public void DeleteteRanking(int Id);
    public IEnumerable<Ranking> GetRankingsOfAnActivity(int ActivityId);
    public Ranking GetRankingOfActivityByUsername(int ActivityId, string userName);
    public IEnumerable<Ranking> GetRankingsByUsername(string userName);
    public Ranking GetRankingById(int Id);
}