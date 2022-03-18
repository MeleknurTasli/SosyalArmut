namespace Armut.Model
{
    public class City
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CountryId { get; set; }
        public int? StateId { get; set; }
        public virtual Country? Country { get; set; }
        public virtual State? State { get; set; }
        public bool Visibility { get; set; }
        public virtual ICollection<District>? Districts { get; set; }

        public virtual ICollection<Address>? Addresses { get; set; }

        //public virtual ICollection<Neighbourhood>? Neighbourhoods { get; set; }
    }
}