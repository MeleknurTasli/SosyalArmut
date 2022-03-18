namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class CountryController : ControllerBase
{
    private readonly ICountryService _CountryService;
    public CountryController(ICountryService CountryService)
    {
        _CountryService = CountryService;
    }

    [HttpPost]
    public async Task<Country> CreateCountry(Country Country)
    {
        return await _CountryService.CreateCountry(Country);
    }

    [HttpDelete("{Id}")]
    public async Task DeleteCountry(int Id)
    {
        await _CountryService.DeleteCountry(Id);
    }

    [HttpGet]
    public async Task<IEnumerable<Country>> GetAllCountries()
    {
        return await _CountryService.GetAllCountries();
    }

    [HttpGet("{Id}")]
    public async Task<Country> GetCountryById(int Id)
    {
        return await _CountryService.GetCountryById(Id);
    }

    [HttpGet("Name")]
    public async Task<Country> GetCountryByName(string Name)
    {
        return await _CountryService.GetCountryByName(Name);
    }

    [HttpPut]
    public async Task<Country> UpdateCountry(Country Country)
    {
        return await _CountryService.UpdateCountry(Country);
    }
}