public class CommentDTO
{
    public int Id { get; set; }
    public string Title  { get; set; }
    public string Description { get; set; }
    public bool Visibility { get; set; }
    public DateTime? EntryDate { get; set; }
    public int ActivityId { get; set; }
    public CommentDTO()
    {
        
    }
    public CommentDTO(Comment _comment)
    {
        Id = _comment.Id;
        Title = _comment.Title;
        Description = _comment.Description;
        Visibility = _comment.Visibility;
        EntryDate = _comment.EntryDate;
        ActivityId = _comment.ActivityId;
    }
}