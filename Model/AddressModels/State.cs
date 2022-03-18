namespace Armut.Model
{
    public class State
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int CountryId { get; set; }
        public virtual Country? Country { get; set; }
        public virtual ICollection<City>? Cities { get; set; }
        public bool Visibility { get; set; }
        public virtual ICollection<Address>? Addresses { get; set; }


        //public virtual ICollection<District>? Districts { get; set; }
        //public virtual ICollection<Neighbourhood>? Neighbourhoods { get; set; }
    }
}