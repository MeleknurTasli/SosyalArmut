namespace Armut.Model
{
    public class Neighbourhood
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int DistrictId { get; set; }
        public bool Visibility { get; set; }
        public virtual District? District {get; set;}

        public virtual ICollection<Address>? Addresses {get;set;}
    }
}