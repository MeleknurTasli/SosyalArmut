public class CommentRepository : ICommentRepository
{
    BaseDBContext _BaseDBContext;
    public CommentRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }
    public Comment CreateComment(CreateCommentDTO createCommentDTO)
    {
        Activity? _Activity = _BaseDBContext.Activities.Where(x => x.Id == createCommentDTO.ActivityId).FirstOrDefault();
        User? _User = _BaseDBContext.Users.Where(x => x.Id == createCommentDTO.UserId).FirstOrDefault();
        if (_Activity != null && _User != null)
        {
            Comment comment = new Comment(createCommentDTO);
            _BaseDBContext.Comments.Add(comment);
            _BaseDBContext.SaveChanges();
            return comment;
        }
        return null;
    }

    public IEnumerable<Comment> GetAllCommentsByActivityId(int ActivityID)
    {
        return _BaseDBContext.Comments.Where(x => x.ActivityId == ActivityID).ToList();
    }

    public IEnumerable<Comment> GetAllCommentsByUserId(int userID)
    {
        return _BaseDBContext.Comments.Where(x => x.UserId == userID).ToList();
    }

    public Comment GetCommentById(int Id)
    {
        return _BaseDBContext.Comments.Where(x => x.Id == Id).FirstOrDefault();
    }

    public Comment UpdateComment(int CommentId, UpdateCommentDTO updateCommentDTO)
    {
        Comment? comment = _BaseDBContext.Comments.Where(x => x.Id == CommentId).FirstOrDefault();
        comment.Description = updateCommentDTO.Description;
        comment.Title = updateCommentDTO.Title;
        comment.Visibility = updateCommentDTO.Visibility;
        _BaseDBContext.SaveChanges();
        return comment;
    }
}