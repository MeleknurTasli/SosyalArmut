public interface IAccountService
{
    public Task<ActionResult<AccountDTO>> FindAccountByEmailAndPassword(LoginDTO loginDTO);

    public Task<ActionResult<AccountDTO>> FindAccountById(int id);


    public Task<ActionResult<AccountDTO>> CreateAccount(LoginDTO loginDTO);

    public Task<ActionResult<AccountDTO>> UpdateEmail(string newEmail, LoginDTO loginDTO);

    public Task ChangeAccountVisibility(int Id);

    public Task<ActionResult<AccountDTO>> FindAccountByEmail(string Email);
    public Task<IEnumerable<AccountDTO>> GetAllAccounts();
}