namespace Armut.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool HasStates { get; set; }
        public bool Visibility { get; set; }
        public virtual ICollection<State>? States {get;set;}
        public virtual ICollection<City>? Cities {get;set;}
        public virtual ICollection<Address>? Addresses {get;set;}
        
        //public virtual ICollection<District>? Districts {get;set;}
        //public virtual ICollection<Neighbourhood>? Neighbourhoods {get;set;}
    }
}