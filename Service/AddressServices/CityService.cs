public class CityService : ICityService
{
    private readonly ICityRepository _ICityRepository;
    public CityService(ICityRepository _ICityRepository)
    {
        this._ICityRepository = _ICityRepository;
    }

    public async Task<City> CreateCity(City City)
    {
        try
        {
            City _City = await _ICityRepository.GetCityById(City.Id);
            if(_City == null)
            {
                return await _ICityRepository.CreateCity(City);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteCity(int Id)
    {
        try
        {
            City _City = await _ICityRepository.GetCityById(Id);
            if (_City != null)
            {
                await _ICityRepository.DeleteCity(Id);
            }

        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<City>> GetAllCities()
    {
        try
        {
            return await _ICityRepository.GetAllCities();
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<City>> GetAllCitiesByStateName(string StateName)
    {
        try
        {
            return await _ICityRepository.GetAllCitiesByStateName(StateName);
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<City>> GetAllCitiesByCountryName(string CountryName)
    {
        try
        {
            return await _ICityRepository.GetAllCitiesByCountryName(CountryName);
        }
        catch
        {
            throw;
        }
    }

    public async Task<City> GetCityById(int Id)
    {
        try
        {
            return await _ICityRepository.GetCityById(Id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<City> GetCityByName(string Name)
    {
        try
        {
            return await _ICityRepository.GetCityByName(Name);
        }
        catch
        {
            throw;
        }
    }

    public async Task<City> UpdateCity(City City)
    {
        try
        {
            City _City = await _ICityRepository.GetCityById(City.Id);
            if (_City != null)
            {
                return await _ICityRepository.UpdateCity(City);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }
}