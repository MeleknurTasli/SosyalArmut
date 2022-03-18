namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class NeighbourhoodController : ControllerBase
{
    private readonly INeighbourhoodService _NeighbourhoodService;
    public NeighbourhoodController(INeighbourhoodService NeighbourhoodService)
    {
        _NeighbourhoodService = NeighbourhoodService;
    }

    [HttpPost]
    public async Task<Neighbourhood> CreateNeighbourhood(Neighbourhood Neighbourhood)
    {
        return await _NeighbourhoodService.CreateNeighbourhood(Neighbourhood);
    }

    [HttpDelete("{Id}")]
    public async Task DeleteNeighbourhood(int Id)
    {
        await _NeighbourhoodService.DeleteNeighbourhood(Id);
    }

    [HttpGet]
    public async Task<IEnumerable<Neighbourhood>> GetAllNeighbourhoods()
    {
        return await _NeighbourhoodService.GetAllNeighbourhoods();
    }

    [HttpGet("DistrictName")]
    public async Task<IEnumerable<Neighbourhood>> GetAllNeighbourhoodsByDistrictName(string DistrictName)
    {
        return await _NeighbourhoodService.GetAllNeighbourhoodsByDistrictName(DistrictName);
    }

    [HttpGet("{Id}")]
    public async Task<Neighbourhood> GetNeighbourhoodById(int Id)
    {
        return await _NeighbourhoodService.GetNeighbourhoodById(Id);
    }

    [HttpGet("Name")]
    public async Task<Neighbourhood> GetNeighbourhoodByName(string Name)
    {
        return await _NeighbourhoodService.GetNeighbourhoodByName(Name);
    }

    [HttpPut]
    public async Task<Neighbourhood> UpdateNeighbourhood(Neighbourhood Neighbourhood)
    {
        return await _NeighbourhoodService.UpdateNeighbourhood(Neighbourhood);
    }
}