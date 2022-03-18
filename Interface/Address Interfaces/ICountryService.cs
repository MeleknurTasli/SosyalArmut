public interface ICountryService
{
    public Task<Country> GetCountryById(int Id);
    public Task<Country> GetCountryByName(string Name);
    public Task<IEnumerable<Country>> GetAllCountries();
    public Task<Country> CreateCountry(Country Country);
    public Task DeleteCountry(int Id);
    public Task<Country> UpdateCountry(Country Country);
    
}