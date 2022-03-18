namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class StateController : ControllerBase
{
    private readonly IStateService _StateService;
    public StateController(IStateService StateService)
    {
        _StateService = StateService;
    }

    [HttpPost]
    public async Task<State> CreateState(State State)
    {
        return await _StateService.CreateState(State);
    }

    [HttpDelete("{Id}")]
    public async Task DeleteState(int Id)
    {
        await _StateService.DeleteState(Id);
    }

    [HttpGet]
    public async Task<IEnumerable<State>> GetAllStates()
    {
        return await _StateService.GetAllStates();
    }

    [HttpGet("CountryName")]
    public async Task<IEnumerable<State>> GetAllStatesByCountryName(string CountryName)
    {
        return await _StateService.GetAllStatesByCountryName(CountryName);
    }

    [HttpGet("{Id}")]
    public async Task<State> GetStateById(int Id)
    {
        return await _StateService.GetStateById(Id);
    }

    [HttpGet("Name")]
    public async Task<State> GetStateByName(string Name)
    {
        return await _StateService.GetStateByName(Name);
    }

    [HttpPut]
    public async Task<State> UpdateState(State State)
    {
        return await _StateService.UpdateState(State);
    }
}