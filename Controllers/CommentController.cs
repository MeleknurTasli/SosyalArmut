namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly ICommentService _commentService;
    public CommentController(ICommentService commentService)
    {
        _commentService = commentService;
    }

    [HttpPost]
    public async Task<ActionResult<CommentDTO>> CreateComment(CreateCommentDTO createCommentDTO)
    {
        return await _commentService.CreateComment(createCommentDTO);
    }

    [HttpGet("ActivityId")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllCommentsByActivityId(int ActivityID)
    {
        return await _commentService.GetAllCommentsByActivityId(ActivityID);
    }

    [HttpGet("UserId")]
    public async Task<ActionResult<IEnumerable<CommentDTO>>> GetAllCommentsByUserId(int userID)
    {
        return await _commentService.GetAllCommentsByUserId(userID);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<CommentDTO>> GetCommentById(int Id)
    {
        return await _commentService.GetCommentById(Id);
    }

    [HttpPut("{CommentId}")]
    public async Task<ActionResult<CommentDTO>> UpdateComment(int CommentId, UpdateCommentDTO updateCommentDTO)
    {
        return await _commentService.UpdateComment(CommentId, updateCommentDTO);
    }
}