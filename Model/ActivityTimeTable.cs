namespace Armut.Model
{
    public class ActivityTimeTable
    {
        public int Id { get; set; }
        public DateTime? StartDate { get; set; } //=new DateTime(1,1,1,1,1,1);
        public DateTime? EndDate { get; set; }
        public int ActivityId { get; set; }
        public virtual Activity? Activity { get; set; }
        public int Quota {get; set;}
        public int NumberOfAttendants {get; set;}
        public bool IsUpComing { get; set; }
        public bool Visibility { get; set; }

        ////public int? PriceTableId {get; set;}
        ////public virtual PriceTable? Price {get; set;}

        public virtual IEnumerable<UserActivityTimeTable>? AttendantUsers { get; set; }
        public virtual List<User>? AllAttendantUsers {get; set; }

        public ActivityTimeTable()
        {
            //AllAttendantUsers = new List<User>();
        }

        public ActivityTimeTable(CreateUpdateActivityTimeTableDTO _dto)
        {
            StartDate = _dto.StartDate;
            EndDate = _dto.EndDate;
            ActivityId = _dto.ActivityId;
            Quota = _dto.Quota;
            IsUpComing = true;
            Visibility = _dto.Visibility;
        }
    }
}
