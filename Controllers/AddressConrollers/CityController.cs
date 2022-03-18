namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class CityController : ControllerBase
{
    private readonly ICityService _CityService;
    public CityController(ICityService CityService)
    {
        _CityService = CityService;
    }

    [HttpPost]
    public async Task<City> CreateCity(City City)
    {
        return await _CityService.CreateCity(City);
    }

    [HttpDelete("{Id}")]
    public async Task DeleteCity(int Id)
    {
        await _CityService.DeleteCity(Id);
    }

    [HttpGet]
    public async Task<IEnumerable<City>> GetAllCities()
    {
        return await _CityService.GetAllCities();
    }

    [HttpGet("StateName")]
    public async Task<IEnumerable<City>> GetAllCitiesByStateName(string StateName)
    {
        return await _CityService.GetAllCitiesByStateName(StateName);
    }

    [HttpGet("CountryName")]
    public async Task<IEnumerable<City>> GetAllCitiesByCountryName(string CountryName)
    {
        return await _CityService.GetAllCitiesByCountryName(CountryName);
    }

    [HttpGet("{Id}")]
    public async Task<City> GetCityById(int Id)
    {
        return await _CityService.GetCityById(Id);
    }

    [HttpGet("Name")]//https://localhost:7293/City?city=Istanbul
    public async Task<City> GetCityByName(string Name)
    {
        return await _CityService.GetCityByName(Name);
    }

    [HttpPut]
    public async Task<City> UpdateCity(City City)
    {
        return await _CityService.UpdateCity(City);
    }
}