public class ActivityDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public bool? Visibility { get; set; }
    public DateTime? CreatedTime { get; set; }
    public double? Price { get; set; }
    public double? Point { get; set; }
    public int SubCategoryId { get; set; }
    public virtual SubCategory? SubCategory { get; set; }

    public int AddressId { get; set; }
    public virtual Address? Address { get; set; }

    //public int OwnerUserId { get; set; }
    public virtual User? OwnerUser { get; set; }


    public virtual IEnumerable<ActivityTimeTable>? TimeTable { get; set; }


    public ActivityDTO() {}
   
    public ActivityDTO(Activity _Activity)
    {
        Id = _Activity.Id;
        Name = _Activity.Name;
        Description = _Activity.Description;
        Image = _Activity.Image;
        CreatedTime = _Activity.CreatedTime;
        Visibility = _Activity.Visibility;
        Price = _Activity.Price;
        Point = _Activity.Point;
        SubCategoryId = _Activity.SubCategoryId;
        AddressId = _Activity.AddressId;
        //OwnerUserId = _Activity.OwnerUserId;
        SubCategory = _Activity.SubCategory;
        Address = _Activity.Address;
        OwnerUser = _Activity.OwnerUser;
        TimeTable = _Activity.TimeTable;
    }
}