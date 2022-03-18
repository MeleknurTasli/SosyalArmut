public class StateRepository : IStateRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public StateRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }
    
    public async Task<State> CreateState(State State)
    {
        await _BaseDBContext.States.AddAsync(State);
        await _BaseDBContext.SaveChangesAsync();
        return State;
    }

    public async Task DeleteState(int Id)
    {
        State State = await GetStateById(Id);
        _BaseDBContext.States.Remove(State);
        await _BaseDBContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<State>> GetAllStates()
    {
         return await _BaseDBContext.States.ToListAsync();
    }

    public async Task<State> GetStateById(int Id)
    {
         return await _BaseDBContext.States.FirstOrDefaultAsync(e=>e.Id == Id);
    }

    public async Task<State> GetStateByName(string Name)
    {
         return await _BaseDBContext.States.FirstOrDefaultAsync(e=>e.Name == Name);
    }

    public async Task<State> UpdateState(State UpdatedState)
    {
        State FoundState = await _BaseDBContext.States.FindAsync(UpdatedState.Id);
        FoundState.Name = UpdatedState.Name;
        FoundState.Visibility = UpdatedState.Visibility;
        FoundState.CountryId = UpdatedState.CountryId;
        await _BaseDBContext.SaveChangesAsync();
        return UpdatedState;
    }

    public async Task<IEnumerable<State>> GetAllStatesByCountryName(string CountryName)
    {
        return await _BaseDBContext.States.Where(e=>e.Country.Name == CountryName).ToListAsync();
    }
}