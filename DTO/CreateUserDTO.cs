public class CreateUserDTO
{
    public string Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDay { get; set; }
    public string? ProfilePhoto { get; set; }
    public string? PhoneNumber { get; set; }
    public int AccountId { get; set; }
    public int GenderId { get; set; }
    public List<int>? RoleIds {get; set;}  
}