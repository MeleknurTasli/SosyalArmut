public class RankingDTO
{
    public int Id { get; set; }
    public int ActivityId { get; set; }
    //public Activity? Activity { get; set; }
    //public int? RatingUserId { get; set; }
    //public virtual User? RatingUser { get; set; }
    public double? Value { get; set; }
    public RankingDTO(Ranking _Ranking)
    {
        Id = _Ranking.Id;
        ActivityId = _Ranking.ActivityId;
        //Activity = _Ranking.Activity;
        //RatingUser = _Ranking.RatingUser;
        Value = _Ranking.Value;
    }
}