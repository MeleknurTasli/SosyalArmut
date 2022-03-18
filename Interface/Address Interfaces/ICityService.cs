public interface ICityService
{
    public Task<City> GetCityById(int Id);
    public Task<City> GetCityByName(string Name);
    public Task<IEnumerable<City>> GetAllCities();
    public Task<IEnumerable<City>> GetAllCitiesByStateName(string StateName);
     public Task<IEnumerable<City>> GetAllCitiesByCountryName(string CountryName);
    public Task<City> CreateCity(City City);
    public Task DeleteCity(int Id);
    public Task<City> UpdateCity(City City);
}