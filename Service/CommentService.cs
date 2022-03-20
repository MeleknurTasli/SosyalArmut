public class CommentService : ControllerBase, ICommentService
{
    private readonly ICommentRepository _commentRepository;
    public CommentService(ICommentRepository commentRepository)
    {
        _commentRepository = commentRepository;
    }

    public async Task<ActionResult<CommentDTO>> CreateComment(CreateCommentDTO createCommentDTO)
    {
        try
        {
            Comment comment = _commentRepository.CreateComment(createCommentDTO);
            if (comment == null)
            {
                return BadRequest("UserId ve ActivityId kontrol ediniz.");
            }
            return new CommentDTO(comment);
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllCommentsByActivityId(int ActivityID)
    {
        try
        {
            var comments = _commentRepository.GetAllCommentsByActivityId(ActivityID);
            return ConvertToCommentDTO(comments);
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllCommentsByUserId(int userID)
    {
        try
        {
            var comments = _commentRepository.GetAllCommentsByUserId(userID);
            return ConvertToCommentDTO(comments);
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<CommentDTO>> GetCommentById(int Id)
    {
        try
        {
            Comment comment = _commentRepository.GetCommentById(Id);
            if (comment == null)
            {
                return BadRequest("Bu Id ile bir comment mevcut değildir..");
            }
            return new CommentDTO(comment);
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<CommentDTO>> UpdateComment(int CommentId, UpdateCommentDTO updateCommentDTO)
    {
        try
        {
            if (_commentRepository.GetCommentById(CommentId) == null)
            {
                return BadRequest("Bu Id ile bir comment mevcut değildir.");
            }
            Comment comment = _commentRepository.UpdateComment(CommentId, updateCommentDTO);
            return new CommentDTO(comment);
        }
        catch
        {
            throw;
        }
    }

    private List<CommentDTO> ConvertToCommentDTO(IEnumerable<Comment> comments)
    {
        List<CommentDTO> CommentDTOs = new List<CommentDTO>();
        foreach (Comment comment in comments)
        {
            CommentDTOs.Add(new CommentDTO(comment));
        }
        return CommentDTOs;
    }
}