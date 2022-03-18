public class CreateUserActivityTimeTableDTO
{
    public int? UserId { get; set; }
    public virtual User? User { get; set; }
    public int? ActivityTimeTableId { get; set; }
    public bool Visibility { get; set; }
    public ActivityTimeTable? ActivityTimeTable { get; set; }
    public CreateUserActivityTimeTableDTO(UserActivityTimeTable _usertime)
    {
        UserId = _usertime.UserId;
        ActivityTimeTableId = _usertime.ActivityTimeTableId;
        User = _usertime.User;
        ActivityTimeTable = _usertime.ActivityTimeTable;
        Visibility = _usertime.Visibility;
    }
    public CreateUserActivityTimeTableDTO()
    {
        
    }
}