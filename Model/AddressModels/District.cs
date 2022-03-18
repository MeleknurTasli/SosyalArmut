namespace Armut.Model
{
    public class District
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CityId { get; set; }
        public virtual City? City {get; set;}
        public bool Visibility { get; set; }

        public virtual ICollection<Neighbourhood>? Neighbourhoods { get; set; }

        public virtual ICollection<Address>? Addresses {get;set;}
    }
}