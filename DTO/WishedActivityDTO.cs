public class WishedActivityDTO
{
    public int Id { get; set; }
    public int? ActivityId { get; set; }
    //public Activity? Activity { get; set; }
    //public virtual User? User { get; set; }

    public WishedActivityDTO(WishedActivity wishedActivity)
    {
        Id = wishedActivity.Id;
        ActivityId = wishedActivity.ActivityId;
        //Activity = wishedActivity.Activity;
        //User = wishedActivity.User;
    }
}