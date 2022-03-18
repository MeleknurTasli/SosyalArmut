public class AccountService : ControllerBase, IAccountService
{
   private readonly IAccountRepository _AccountRepository;
    public AccountService(IAccountRepository AccountRepository)
    {
        _AccountRepository = AccountRepository;
    }

    public async Task ChangeAccountVisibility(int Id)
    {
        if(_AccountRepository.FindAccountById(Id) != null)
        {
             _AccountRepository.ChangeAccountVisibility(Id);
        }
    }

    public async Task<ActionResult<AccountDTO>> CreateAccount(LoginDTO loginDTO)
    {
        if(_AccountRepository.FindAccountByEmail(loginDTO.Email) == null)
        {
            return Ok(_AccountRepository.CreateAccount(loginDTO));
        }
        return BadRequest("Hata : Bu emaile sahip kullanıcı mevcuttur.");
    }

    public async Task<ActionResult<AccountDTO>> FindAccountByEmailAndPassword(LoginDTO loginDTO)
    {
        var acc = _AccountRepository.FindAccountByEmailAndPassword(loginDTO);
        if(acc != null) return new AccountDTO(acc);
        return BadRequest("Mevcut değildir");
    }

    public async Task<ActionResult<AccountDTO>> FindAccountById(int Id)
    {
        var acc = _AccountRepository.FindAccountById(Id);
        if(acc != null) return new AccountDTO(acc);
        return BadRequest("Mevcut değildir");
    }

    public async Task<ActionResult<AccountDTO>> UpdateEmail(string newEmail, LoginDTO loginDTO)
    {
        if(_AccountRepository.FindAccountByEmailAndPassword(loginDTO) != null)
        {
            var account = _AccountRepository.UpdateEmail(newEmail, loginDTO);
            if(account == null) return BadRequest("Account bulunamadı.");
            return new AccountDTO(account);
        }
        return BadRequest("Hata : Email ya da şifre hatalı girilmiştir.");
    }

    public async Task<ActionResult<AccountDTO>> FindAccountByEmail(string Email)
    {
        var acc = _AccountRepository.FindAccountByEmail(Email);
        if(acc != null) return new AccountDTO(acc);
        return BadRequest("Mevcut değildir.");
    }

    public async Task<IEnumerable<AccountDTO>> GetAllAccounts()
    {
        List<AccountDTO> accDTOs = new List<AccountDTO>();
        foreach(Account acc in _AccountRepository.GetAllAccounts())
        {
            accDTOs.Add(new AccountDTO(acc));
        }
        return accDTOs;
    }
}