namespace Armut.Model
{
    public class WishedActivity
    {
        public int Id { get; set; }
        public int ActivityId { get; set; }  
        public Activity? Activity { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }       
    }
}