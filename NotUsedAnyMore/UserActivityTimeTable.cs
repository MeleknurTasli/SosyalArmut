namespace Armut.Model
{
    public class UserActivityTimeTable  //kayıt tablosu
    {
        //public int Id { get; set; }
        public int? UserId { get; set; }
        public virtual User? User { get; set; }
        public int? ActivityTimeTableId { get; set; }
        public ActivityTimeTable? ActivityTimeTable { get; set; }
        public bool Visibility { get; set; }
        public bool IsUpComing { get; set; }

        //public bool IsAttended { get; set; }

        public UserActivityTimeTable()
        {

        }

        public UserActivityTimeTable(CreateUserActivityTimeTableDTO dto)
        {
            UserId = dto.UserId;
            ActivityTimeTableId = dto.ActivityTimeTableId;
            User = dto.User;
            ActivityTimeTable = dto.ActivityTimeTable;
            IsUpComing = true;
            Visibility = dto.Visibility;
        }

    }
}