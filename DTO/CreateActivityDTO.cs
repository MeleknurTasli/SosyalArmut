public class CreateActivityDTO
{
    //public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public double? Price { get; set; }
    public int SubCategoryId { get; set; }
    //public virtual SubCategory? SubCategory { get; set; }

    public int AddressId { get; set; }
    //public virtual Address? Address { get; set; }

    public int OwnerUserId { get; set; }
    //public virtual User? OwnerUser { get; set; }


    //public virtual IEnumerable<ActivityTimeTable>? TimeTable { get; set; }


    public CreateActivityDTO() {}
   
    public CreateActivityDTO(Activity _Activity)
    {
        //Id = _Activity.Id;
        Name = _Activity.Name;
        Description = _Activity.Description;
        Image = _Activity.Image;
        Price = _Activity.Price;
        SubCategoryId = _Activity.SubCategoryId;
        AddressId = _Activity.AddressId;
        OwnerUserId = _Activity.OwnerUserId;
        /*
        SubCategory = _Activity.SubCategory;
        Address = _Activity.Address;
        OwnerUser = _Activity.OwnerUser;
        TimeTable = _Activity.TimeTable;*/
    }
}