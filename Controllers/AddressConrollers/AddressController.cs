namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _AddressService;
    public AddressController(IAddressService AddressService)
    {
        _AddressService = AddressService;
    }

    [HttpPost]
    public async Task<Address> CreateAddress(Address Address)
    {
        return await _AddressService.CreateAddress(Address);
    }

    [HttpGet]
    public async Task<IEnumerable<Address>> GetAllAddresses()
    {
        return await _AddressService.GetAllAddresses();
    }

    [HttpGet("{Id}")]
    public async Task<Address> GetAddressById(int Id)
    {
        return await _AddressService.GetAddressById(Id);
    }

    [HttpPut]
    public async Task<Address> UpdateAddress(Address Address)
    {
        return await _AddressService.UpdateAddress(Address);
    }

    [Route("[action]")]
    [HttpGet]  //https://localhost:7293/Address/GetAllAddressesByCity
    public async Task<IEnumerable<Address>> GetAllAddressesByCity(City city)
    {
        return await _AddressService.GetAllAddressesByCity(city);
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<IEnumerable<Address>> GetAllAddressesByCountry(Country country)
    {
        return await _AddressService.GetAllAddressesByCountry(country);
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<IEnumerable<Address>> GetAllAddressesByDistrict(District district)
    {
        return await _AddressService.GetAllAddressesByDistrict(district);
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<IEnumerable<Address>> GetAllAddressesByState(State state)
    {
        return await _AddressService.GetAllAddressesByState(state);
    }

    [Route("[action]")]
    [HttpGet]
    public async Task<IEnumerable<Address>> GetAllAddressesByNeighbourhood(Neighbourhood neighbourhood)
    {
        return await _AddressService.GetAllAddressesByNeighbourhood(neighbourhood);
    }
}