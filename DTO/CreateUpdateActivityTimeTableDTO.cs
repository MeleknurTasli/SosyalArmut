public class CreateUpdateActivityTimeTableDTO
{
    public DateTime? StartDate { get; set; } 
    public DateTime? EndDate { get; set; }
    public int ActivityId { get; set; }
    public int Quota { get; set; }
    public bool Visibility { get; set; }
}