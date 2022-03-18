public class CountryService : ICountryService
{
    private readonly ICountryRepository _ICountryRepository;
    public CountryService(ICountryRepository _ICountryRepository)
    {
        this._ICountryRepository = _ICountryRepository;
    }

    public async Task<Country> CreateCountry(Country Country)
    {
        try
        {
            Country _Country = await _ICountryRepository.GetCountryById(Country.Id);
            if(_Country == null)
            {
                return await _ICountryRepository.CreateCountry(Country);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteCountry(int Id)
    {
        try
        {
            Country _Country = await _ICountryRepository.GetCountryById(Id);
            if (_Country != null)
            {
                await _ICountryRepository.DeleteCountry(Id);
            }

        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Country>> GetAllCountries()
    {
        try
        {
            return await _ICountryRepository.GetAllCountries();
        }
        catch
        {
            throw;
        }
    }

    public async Task<Country> GetCountryById(int Id)
    {
        try
        {
            return await _ICountryRepository.GetCountryById(Id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Country> GetCountryByName(string Name)
    {
        try
        {
            return await _ICountryRepository.GetCountryByName(Name);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Country> UpdateCountry(Country Country)
    {
        try
        {
            Country _Country = await _ICountryRepository.GetCountryById(Country.Id);
            if (_Country != null)
            {
                return await _ICountryRepository.UpdateCountry(Country);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }
}