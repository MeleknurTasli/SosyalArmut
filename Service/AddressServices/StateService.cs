public class StateService : IStateService
{
    private readonly IStateRepository _IStateRepository;
    public StateService(IStateRepository _IStateRepository)
    {
        this._IStateRepository = _IStateRepository;
    }

    public async Task<State> CreateState(State State)
    {
        try
        {
            State _State = await _IStateRepository.GetStateById(State.Id);
            if(_State == null)
            {
                return await _IStateRepository.CreateState(State);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteState(int Id)
    {
        try
        {
            State _State = await _IStateRepository.GetStateById(Id);
            if (_State != null)
            {
                await _IStateRepository.DeleteState(Id);
            }

        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<State>> GetAllStates()
    {
        try
        {
            return await _IStateRepository.GetAllStates();
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<State>> GetAllStatesByCountryName(string CountryName)
    {
        try
        {
            return await _IStateRepository.GetAllStatesByCountryName(CountryName);
        }
        catch
        {
            throw;
        }
    }

    public async Task<State> GetStateById(int Id)
    {
        try
        {
            return await _IStateRepository.GetStateById(Id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<State> GetStateByName(string Name)
    {
        try
        {
            return await _IStateRepository.GetStateByName(Name);
        }
        catch
        {
            throw;
        }
    }

    public async Task<State> UpdateState(State State)
    {
        try
        {
            State _State = await _IStateRepository.GetStateById(State.Id);
            if (_State != null)
            {
                return await _IStateRepository.UpdateState(State);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }
}