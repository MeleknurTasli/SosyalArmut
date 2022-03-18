public class AccountRepository : IAccountRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public AccountRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public Account CreateAccount(LoginDTO loginDTO)
    {
        Account account = new Account()
        {
            Email = loginDTO.Email,
            Password = loginDTO.Password,
            Visibility = true
        };

        _BaseDBContext.Accounts.Add(account);
        _BaseDBContext.SaveChanges();
        return account;
    }

    public Account FindAccountByEmailAndPassword(LoginDTO loginDTO)
    {
        Account FoundAccount = (from x in _BaseDBContext.Accounts
                                 where x.Email == loginDTO.Email && x.Password == loginDTO.Password && x.Visibility == true
                                 select x).FirstOrDefault();
        return FoundAccount;
    }

    public Account FindAccountById(int Id)
    {
        Account account= (from x in _BaseDBContext.Accounts
                               where x.Id == Id
                               select x).FirstOrDefault();
        return account;
    }

    public Account FindAccountByEmail(string Email)
    {
        Account account= (from x in _BaseDBContext.Accounts
                               where x.Email == Email
                               select x).FirstOrDefault();
        return account;
    }

    public Account UpdateEmail(string newEmail, LoginDTO loginDTO)
    {
        Account account = (from x in _BaseDBContext.Accounts
                           where x.Email == loginDTO.Email
                           select x).FirstOrDefault();
        if (account == null ) return null;
        account.Email = newEmail;
        _BaseDBContext.SaveChanges();
        return account;
    }

    public void ChangeAccountVisibility(int Id)
    {
        Account account = FindAccountById(Id);
        if (account != null)
        {
            List<Activity> createdActivities = _BaseDBContext.Activities.Where(x => x.OwnerUser.AccountId == Id).ToList();
            foreach (Activity item in createdActivities)
            {
                item.Visibility = false;
                _BaseDBContext.SaveChanges();
            }
            account.Visibility = false;
            _BaseDBContext.SaveChanges();
        }
    }
    public IEnumerable<Account> GetAllAccounts()
    {
        return _BaseDBContext.Accounts.ToList();
    }
}