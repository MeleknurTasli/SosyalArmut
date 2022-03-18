public class CountryRepository : ICountryRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public CountryRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public async Task<Country> CreateCountry(Country Country)
    {
        await _BaseDBContext.Countries.AddAsync(Country);
        await _BaseDBContext.SaveChangesAsync();
        return Country;
    }

    public async Task DeleteCountry(int Id)
    {
        Country Country = await GetCountryById(Id);
        _BaseDBContext.Countries.Remove(Country);
        await _BaseDBContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Country>> GetAllCountries()
    {
         return await _BaseDBContext.Countries.ToListAsync();
    }

    public async Task<Country> GetCountryById(int Id)
    {
         return await _BaseDBContext.Countries.FirstOrDefaultAsync(e=>e.Id == Id);
    }

    public async Task<Country> GetCountryByName(string Name)
    {
         return await _BaseDBContext.Countries.FirstOrDefaultAsync(e=>e.Name == Name);
    }

    public async Task<Country> UpdateCountry(Country UpdatedCountry)
    {
        Country FoundCountry = await _BaseDBContext.Countries.FindAsync(UpdatedCountry.Id);
        FoundCountry.Name = UpdatedCountry.Name;
        FoundCountry.HasStates = UpdatedCountry.HasStates;
        FoundCountry.Visibility = UpdatedCountry.Visibility;
        await _BaseDBContext.SaveChangesAsync();
        return UpdatedCountry;
    }
}