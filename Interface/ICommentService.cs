public interface ICommentService
{
    public Task<ActionResult<CommentDTO>> CreateComment(CreateCommentDTO createCommentDTO);
    public Task<ActionResult<CommentDTO>> UpdateComment(int CommentId, UpdateCommentDTO updateCommentDTO);
    public Task<ActionResult<CommentDTO>> GetCommentById(int Id);
    public Task<ActionResult<IEnumerable<CommentDTO>>> GetAllCommentsByUserId(int userID);
    public Task<ActionResult<IEnumerable<CommentDTO>>> GetAllCommentsByActivityId(int ActivityID);
}