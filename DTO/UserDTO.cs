public class UserDTO
{
    public string? Username { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime BirthDay { get; set; }
    public string? ProfilePhoto { get; set; }
    public string? PhoneNumber { get; set; }

    public virtual Account? Account { get; set; }

    public int GenderId { get; set; }
    public virtual Gender Gender { get; set; }

    public virtual IEnumerable<UserRole> UserRoles { get; set; }
    public virtual IEnumerable<WishedActivity>? WishedActivities { get; set; }
    public virtual IEnumerable<Activity>? CreatedActivities { get; set; }
    public virtual IEnumerable<UserActivityTimeTable>? UserActivityTimeTables { get; set; }
    public virtual IEnumerable<Ranking>? GivenRankings { get; set; }

    public UserDTO(User _User)
    {
        Username = _User.Username;
        FirstName = _User.FirstName;
        LastName = _User.LastName;
        BirthDay = _User.BirthDay;
        ProfilePhoto = _User.ProfilePhoto;
        PhoneNumber = _User.PhoneNumber;
        Account = _User.Account;
        GenderId = _User.GenderId;
        Gender = _User.Gender;
        UserRoles = _User.UserRoles;
        WishedActivities = _User.WishedActivities;
        CreatedActivities = _User.CreatedActivities;
        UserActivityTimeTables = _User.UserActivityTimeTables;
        GivenRankings = _User.GivenRankings;
    }

}