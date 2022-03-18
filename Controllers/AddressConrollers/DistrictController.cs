namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class DistrictController : ControllerBase
{
    private readonly IDistrictService _DistrictService;
    public DistrictController(IDistrictService DistrictService)
    {
        _DistrictService = DistrictService;
    }

    [HttpPost]
    public async Task<District> CreateDistrict(District District)
    {
        return await _DistrictService.CreateDistrict(District);
    }

    [HttpDelete("{Id}")]
    public async Task DeleteDistrict(int Id)
    {
        await _DistrictService.DeleteDistrict(Id);
    }

    [HttpGet]
    public async Task<IEnumerable<District>> GetAllDistricts()
    {
        return await _DistrictService.GetAllDistricts();
    }

    [HttpGet("CityName")]
    public async Task<IEnumerable<District>> GetAllDistrictsByCountryName(string CityName)
    {
        return await _DistrictService.GetAllDistrictsByCityName(CityName);
    }

    [HttpGet("{Id}")]
    public async Task<District> GetDistrictById(int Id)
    {
        return await _DistrictService.GetDistrictById(Id);
    }

    [HttpGet("Name")]
    public async Task<District> GetDistrictByName(string Name)
    {
        return await _DistrictService.GetDistrictByName(Name);
    }

    [HttpPut]
    public async Task<District> UpdateDistrict(District District)
    {
        return await _DistrictService.UpdateDistrict(District);
    }
}