public interface IAccountRepository
{
    public Account FindAccountByEmailAndPassword(LoginDTO loginDTO);

    public Account FindAccountById(int id);


    public Account CreateAccount(LoginDTO loginDTO);

    public Account UpdateEmail(string newEmail, LoginDTO loginDTO);

    public void ChangeAccountVisibility(int Id);
    public Account FindAccountByEmail(string Email);
    public IEnumerable<Account> GetAllAccounts();

}