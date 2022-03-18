public class CityRepository : ICityRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public CityRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public async Task<City> CreateCity(City City)
    {
        await _BaseDBContext.Cities.AddAsync(City);
        await _BaseDBContext.SaveChangesAsync();
        return City;
    }

    public async Task DeleteCity(int Id)
    {
        City City = await GetCityById(Id);
        _BaseDBContext.Cities.Remove(City);
        await _BaseDBContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<City>> GetAllCities()
    {
         return await _BaseDBContext.Cities.ToListAsync();
    }

    public async Task<City> GetCityById(int Id)
    {
         return await _BaseDBContext.Cities.FirstOrDefaultAsync(e=>e.Id == Id);
    }

    public async Task<City> GetCityByName(string Name)
    {
         return await _BaseDBContext.Cities.FirstOrDefaultAsync(e=>e.Name == Name);
    }

    public async Task<City> UpdateCity(City UpdatedCity)
    {
        City FoundCity = await _BaseDBContext.Cities.FindAsync(UpdatedCity.Id);
        FoundCity.Name = UpdatedCity.Name;
        FoundCity.StateId = UpdatedCity.StateId;
        FoundCity.CountryId = UpdatedCity.CountryId;
        FoundCity.Visibility = UpdatedCity.Visibility;
        await _BaseDBContext.SaveChangesAsync();
        return UpdatedCity;
    }

    public async Task<IEnumerable<City>> GetAllCitiesByCountryName(string CountryName)
    {
        return await _BaseDBContext.Cities.Where(x=>x.Country.Name == CountryName).ToListAsync();
    }

    public async Task<IEnumerable<City>> GetAllCitiesByStateName(string StateName)
    {
        return await _BaseDBContext.Cities.Where(e=>e.Country.HasStates == true && e.State.Name == StateName).ToListAsync();
    }


}