public class AddressRepository : IAddressRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public AddressRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public async Task<Address> CreateAddress(Address Address)
    {
        await _BaseDBContext.Addresses.AddAsync(Address);
        await _BaseDBContext.SaveChangesAsync();
        return Address;
    }

    /*
        public async Task DeleteAddress(int Id)
        {
            Address Address = await GetAddressById(Id);
            _BaseDBContext.Addresses.Remove(Address);
            await _BaseDBContext.SaveChangesAsync();
        }
    */

    public async Task<Address> GetAddressById(int Id)
    {
         return await _BaseDBContext.Addresses.FirstOrDefaultAsync(e=>e.Id == Id);
    }

    public async Task<IEnumerable<Address>> GetAllAddresses()
    {
        return await _BaseDBContext.Addresses.ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByCity(City city)
    {
        return await _BaseDBContext.Addresses.Where(x=>x.City.Id == city.Id && x.City.Name == city.Name && x.City.CountryId == city.CountryId).ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByCountry(Country country)
    {
        return await _BaseDBContext.Addresses.Where(x=>x.Country.Id == country.Id && x.Country.Name == country.Name).ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByDistrict(District district)
    {
        return await _BaseDBContext.Addresses.Where(x=>x.District.Id == district.Id && x.District.Name == district.Name && x.District.CityId == district.CityId).ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByNeighbourhood(Neighbourhood neighbourhood)
    {
        return await _BaseDBContext.Addresses.Where(x=>x.Neighbourhood.Id == neighbourhood.Id && x.Neighbourhood.Name == neighbourhood.Name && x.Neighbourhood.DistrictId == neighbourhood.DistrictId).ToListAsync();
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByState(State state)
    {
        return await _BaseDBContext.Addresses.Where(x=>x.State.Id == state.Id && x.State.Name == state.Name && x.State.CountryId == state.CountryId).ToListAsync();
    }

    public async Task<Address> UpdateAddress(Address UpdatedAddress)
    {
        Address FoundAddress = await _BaseDBContext.Addresses.FindAsync(UpdatedAddress.Id);
        FoundAddress.Name = UpdatedAddress.Name;
        FoundAddress.OpenAddress1 = UpdatedAddress.OpenAddress1;
        FoundAddress.OpenAddress2 = UpdatedAddress.OpenAddress2;
        FoundAddress.CountryId = UpdatedAddress.CountryId;
        FoundAddress.CityId = UpdatedAddress.CityId;
        FoundAddress.StateId = UpdatedAddress.StateId;
        FoundAddress.DistrictId = UpdatedAddress.DistrictId;
        FoundAddress.NeighbourhoodId = UpdatedAddress.NeighbourhoodId;
        FoundAddress.Visibility = UpdatedAddress.Visibility;
        await _BaseDBContext.SaveChangesAsync();
        return UpdatedAddress;
    }
}