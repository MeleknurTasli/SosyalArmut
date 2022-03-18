namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _AccountService;

    public AccountController(IAccountService AccountService)
    {
        _AccountService = AccountService;
    }

    [HttpPost]
    public async Task<ActionResult<AccountDTO>> CreateAccount(LoginDTO loginDTO)
    {
        return await _AccountService.CreateAccount(loginDTO);
    }

    [HttpPut("{Id}")]
    public async Task ChangeAccountVisibility(int Id)
    {
        await _AccountService.ChangeAccountVisibility(Id);
    }

    [HttpGet]
    public async Task<ActionResult<AccountDTO>> FindAccountByEmailAndPassword(LoginDTO loginDTO)
    {
        return await _AccountService.FindAccountByEmailAndPassword(loginDTO);
    }

    [HttpGet("{Id}")]
    public async Task<ActionResult<AccountDTO>> FindAccountById(int Id)
    {
        return await _AccountService.FindAccountById(Id);
    }

    [HttpPut("newEmail")]
    public async Task<ActionResult<AccountDTO>> UpdateEmail(string newEmail, LoginDTO loginDTO)
    {
        return await _AccountService.UpdateEmail(newEmail, loginDTO);
    }

    [HttpGet("Email")]
    public async Task<ActionResult<AccountDTO>> FindAccountByEmail(string Email)
    {
        return await _AccountService.FindAccountByEmail(Email);
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<IEnumerable<AccountDTO>> GetAllAccounts()
    {
        return await _AccountService.GetAllAccounts();
    }
}
