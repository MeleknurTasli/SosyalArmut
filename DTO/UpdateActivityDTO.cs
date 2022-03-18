public class UpdateActivityDTO
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }
    public bool? Visibility { get; set; }
    public double? Price { get; set; }
    public int SubCategoryId { get; set; }
    //public virtual SubCategory? SubCategory { get; set; }

    public int AddressId { get; set; }
    //public Address? Address { get; set; }


    public UpdateActivityDTO() {}
   
    public UpdateActivityDTO(Activity _Activity)
    {
        Id = _Activity.Id;
        Name = _Activity.Name;
        Description = _Activity.Description;
        Image = _Activity.Image;
        Visibility = _Activity.Visibility;
        Price = _Activity.Price;
        SubCategoryId = _Activity.SubCategoryId;
        AddressId = _Activity.AddressId;
        /*SubCategory = _Activity.SubCategory;
        Address = _Activity.Address;*/
    }
}