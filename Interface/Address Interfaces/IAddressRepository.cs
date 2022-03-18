public interface IAddressRepository
{
    public Task<Address> GetAddressById(int Id);
    public Task<IEnumerable<Address>> GetAllAddresses();
    public Task<IEnumerable<Address>> GetAllAddressesByCity(City city);
    public Task<IEnumerable<Address>> GetAllAddressesByCountry(Country country);
    public Task<IEnumerable<Address>> GetAllAddressesByDistrict(District district);
    public Task<IEnumerable<Address>> GetAllAddressesByState(State state);
    public Task<IEnumerable<Address>> GetAllAddressesByNeighbourhood(Neighbourhood neighbourhood);
    public Task<Address> CreateAddress(Address Address);
    //public Task DeleteAddress(int Id);
    public Task<Address> UpdateAddress(Address Address);
    
}