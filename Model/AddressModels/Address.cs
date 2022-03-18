namespace Armut.Model
{
    public class Address
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? OpenAddress1 { get; set; }
        public string? OpenAddress2 { get; set; }
        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }
        public int CityId { get; set; }
        public virtual City? City { get; set; }
        public int? StateId { get; set; }
        public virtual State? State { get; set; }
        public int DistrictId { get; set; }
        public virtual District? District { get; set; }
        public int NeighbourhoodId { get; set; }
        public virtual Neighbourhood? Neighbourhood { get; set; }

        public bool Visibility { get; set; }

        public virtual IEnumerable<Activity>? Activities {get; set;}
    }
}