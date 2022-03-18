public class AccountDTO
{
    public int Id { get; set; }

    public string? Email { get; set; }

    public bool? Visibility { get; set; }

    public AccountDTO()
    {
        
    }

    public AccountDTO(Account _account)
    {
        Id = _account.Id;
        Email = _account.Email;
        Visibility = _account.Visibility;
    }
}