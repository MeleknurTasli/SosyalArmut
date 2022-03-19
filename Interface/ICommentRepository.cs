public interface ICommentRepository
{
    public Comment CreateComment(CreateCommentDTO createCommentDTO);
    public Comment UpdateComment(int CommentId, UpdateCommentDTO updateCommentDTO);
    public Comment GetCommentById(int Id);
    public IEnumerable<Comment> GetAllCommentsByUserId(int userID);
    public IEnumerable<Comment> GetAllCommentsByActivityId(int ActivityID);
}