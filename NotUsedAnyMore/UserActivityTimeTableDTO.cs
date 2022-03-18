namespace Armut.Model
{
    public class UserActivityTimeTableDTO  
    {
        public virtual User? User { get; set; }
        public int? ActivityTimeTableId { get; set; }
        public ActivityTimeTable? ActivityTimeTable { get; set; }
        public bool Visibility { get; set; }

        public bool IsUpComing { get; set; }


        public UserActivityTimeTableDTO()
        {

        }

        public UserActivityTimeTableDTO(UserActivityTimeTable activityTimeTable)
        {
            ActivityTimeTableId = activityTimeTable.ActivityTimeTableId;
            User = activityTimeTable.User;
            ActivityTimeTable = activityTimeTable.ActivityTimeTable;
            IsUpComing = activityTimeTable.IsUpComing;
            Visibility = activityTimeTable.Visibility;
        }

    }
}