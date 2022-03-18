public class RankingService : ControllerBase, IRankingService
{
    private readonly IRankingRepository _RankingRepository;
    public RankingService(IRankingRepository _RankingRepository)
    {
        this._RankingRepository = _RankingRepository;
    }

    public async Task<ActionResult<RankingDTO>> CreateRanking(Ranking Ranking)
    {
        try
        {
            bool value=true;
            var Ranks =_RankingRepository.GetRankingsOfAnActivity(Ranking.ActivityId);
            foreach(var item in Ranks)
            {
                if(item.RatingUserId == Ranking.RatingUserId || Ranking.Id == item.Id)
                {
                    value = false;
                    break;
                }
            }
            if (value)
            {
                var ranking = _RankingRepository.CreateRanking(Ranking);
                if (ranking != null)
                    return Ok(new RankingDTO(ranking));
                else return BadRequest("Hata : Id'si girilen ranking değerleri güncellenmelidir.");
            }
            return BadRequest("Hata : Bu kullanıcı, bu aktiveyi zaten oylamıştır. Birden fazla oy kullanılamaz.");
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteteRanking(int Id)
    {
        try
        {
            if ( _RankingRepository.GetRankingById(Id) != null)
            {
                _RankingRepository.DeleteteRanking(Id);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<RankingDTO>> GetAllRankings()
    {
        try
        {
            return ConvertToRankingDTO(_RankingRepository.GetAllRankings());
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<RankingDTO>> GetRankingOfActivityByUsername(int ActivityId, string userName)
    {
        try
        {
            var ranking = _RankingRepository.GetRankingOfActivityByUsername(ActivityId, userName);
            if(ranking != null)
            {
                return new RankingDTO(ranking);
            }
            return BadRequest("Hata : Mevcut değil.");
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<RankingDTO>> GetRankingsByUsername(string userName)
    {
        try
        {
            return ConvertToRankingDTO(_RankingRepository.GetRankingsByUsername(userName));
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<RankingDTO>> GetRankingsOfAnActivity(int ActivityId)
    {
        try
        {
            return ConvertToRankingDTO(_RankingRepository.GetRankingsOfAnActivity(ActivityId));
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<RankingDTO>> UpdateRanking(UpdateRankingDTO Ranking)
    {
        try
        {
            if (_RankingRepository.GetRankingById(Ranking.Id) != null)
            {
                var ranking =_RankingRepository.UpdateRanking(Ranking);
                if(ranking != null)  return Ok(new RankingDTO(ranking));
                return BadRequest("Hata : Id'si girilen ranking değerlerine ait veriler bulunamamıştır.");
                
            }
            else return BadRequest("Hata : Bu Id ile puanlama mevcut değildir.");
        }
        catch
        {
            throw;
        }
    }

    private List<RankingDTO> ConvertToRankingDTO(IEnumerable<Ranking> Rankings)
    {
        List<RankingDTO> RankingDTOs = new List<RankingDTO>();
        foreach(Ranking ranking in Rankings)
        {
            RankingDTOs.Add(new RankingDTO(ranking));
        }
        return RankingDTOs;
    }

}