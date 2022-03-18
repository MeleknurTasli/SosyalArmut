public interface IStateService
{
    public Task<State> GetStateById(int Id);
    public Task<State> GetStateByName(string Name);
    public Task<IEnumerable<State>> GetAllStates();
     public Task<IEnumerable<State>> GetAllStatesByCountryName(string CountryName);
    public Task<State> CreateState(State State);
    public Task DeleteState(int Id);
    public Task<State> UpdateState(State State);
}