namespace Armut.Model
{
    public class Ranking
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }
        public Activity? Activity { get; set; }
        public int RatingUserId { get; set; }
        public virtual User? RatingUser { get; set; }
        public double? Value {get; set;}
    }
}