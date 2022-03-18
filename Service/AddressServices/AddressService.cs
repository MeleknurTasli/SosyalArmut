public class AddressService : IAddressService
{
    private readonly IAddressRepository _IAddressRepository;
    public AddressService(IAddressRepository _IAddressRepository)
    {
        this._IAddressRepository = _IAddressRepository;
    }

    public async Task<Address> CreateAddress(Address Address)
    {
        try
        {
            if(await _IAddressRepository.GetAddressById(Address.Id) == null)
            {
                return await _IAddressRepository.CreateAddress(Address);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }

/*
    public async Task DeleteAddress(int Id)
    {
        try
        {
            if(await _IAddressRepository.GetAddressById(Id) != null)
            {
                await _IAddressRepository.DeleteAddress(Id);
            }
        }
        catch
        {
            throw;
        }
    }
*/

    public async Task<Address> GetAddressById(int Id)
    {
        try
        {
            return await _IAddressRepository.GetAddressById(Id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Address>> GetAllAddresses()
    {
        try
        {
            return await _IAddressRepository.GetAllAddresses();
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByCity(City city)
    {
        try
        {
            return await _IAddressRepository.GetAllAddressesByCity(city);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByCountry(Country country)
    {
        try
        {
            return await _IAddressRepository.GetAllAddressesByCountry(country);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByDistrict(District District)
    {
        try
        {
            return await _IAddressRepository.GetAllAddressesByDistrict(District);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByNeighbourhood(Neighbourhood neighbourhood)
    {
        try
        {
            return await _IAddressRepository.GetAllAddressesByNeighbourhood(neighbourhood);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Address>> GetAllAddressesByState(State state)
    {
        try
        {
            return await _IAddressRepository.GetAllAddressesByState(state);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Address> UpdateAddress(Address Address)
    {
        try
        {
            if(await _IAddressRepository.GetAddressById(Address.Id) != null)
            {
                return await _IAddressRepository.UpdateAddress(Address);
            }
            return null;
            
        }
        catch
        {
            throw;
        }
    }
}