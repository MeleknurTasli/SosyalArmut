namespace Armut.Model
{
    public class Activity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool? Visibility { get; set; }
        public DateTime? CreatedTime { get; set; }
        public double? Price { get; set; }

        public int SubCategoryId { get; set; }
        public virtual SubCategory? SubCategory { get; set; }
        public int AddressId { get; set; }
        public Address? Address { get; set; }


        public int OwnerUserId { get; set; }
        public virtual User? OwnerUser { get; set; }


        public virtual IEnumerable<Ranking>? Ranking { get; set; }
        public virtual IEnumerable<ActivityTimeTable>? TimeTable { get; set; }
        public virtual IEnumerable<WishedActivity>? WishingUsers { get; set; }


        public double? Point { get; set; }




        ////public virtual IEnumerable<PriceTable>? PriceTable { get; set; }

        public Activity()
        {

        }

        public Activity(CreateActivityDTO _Activity)
        {
            Name = _Activity.Name;
            Description = _Activity.Description;
            Image = _Activity.Image;
            Price = _Activity.Price;
            SubCategoryId = _Activity.SubCategoryId;
            AddressId = _Activity.AddressId;
            OwnerUserId = _Activity.OwnerUserId;
            Visibility = true;
            CreatedTime = DateTime.Now;
        }
    }
}
