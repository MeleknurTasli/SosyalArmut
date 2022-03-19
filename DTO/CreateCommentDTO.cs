public class CreateCommentDTO
{
    public string Title  { get; set; }
    public string Description { get; set; }
    public bool Visibility { get; set; }
    public DateTime? EntryDate { get; set; }
    public int UserId { get; set; }
    public int ActivityId { get; set; }
}