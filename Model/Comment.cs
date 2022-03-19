public class Comment
{
    public int Id { get; set; }
    public string Title  { get; set; }
    public string Description { get; set; }
    public bool Visibility { get; set; }
    public DateTime? EntryDate { get; set; }
    public int UserId { get; set; }
    public virtual User User { get; set; }
    public int ActivityId { get; set; }
    public virtual Activity Activity { get; set; }

    public Comment()
    {
        
    }

    public Comment(CreateCommentDTO createCommentDTO)
    {
        Description = createCommentDTO.Description;
        Title = createCommentDTO.Title;
        Visibility = true;
        EntryDate = createCommentDTO.EntryDate;
        UserId = createCommentDTO.UserId;
        ActivityId = createCommentDTO.ActivityId;
    }
}