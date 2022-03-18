public class DistrictRepository : IDistrictRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public DistrictRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public async Task<District> CreateDistrict(District District)
    {
        await _BaseDBContext.Districts.AddAsync(District);
        await _BaseDBContext.SaveChangesAsync();
        return District;
    }

    public async Task DeleteDistrict(int Id)
    {
        District District = await GetDistrictById(Id);
        _BaseDBContext.Districts.Remove(District);
        await _BaseDBContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<District>> GetAllDistricts()
    {
         return await _BaseDBContext.Districts.ToListAsync();
    }

    public async Task<District> GetDistrictById(int Id)
    {
         return await _BaseDBContext.Districts.FirstOrDefaultAsync(e=>e.Id == Id);
    }

    public async Task<District> GetDistrictByName(string Name)
    {
         return await _BaseDBContext.Districts.FirstOrDefaultAsync(e=>e.Name == Name);
    }

    public async Task<District> UpdateDistrict(District UpdatedDistrict)
    {
        District FoundDistrict = await _BaseDBContext.Districts.FindAsync(UpdatedDistrict.Id);
        FoundDistrict.Name = UpdatedDistrict.Name;
        FoundDistrict.Visibility = UpdatedDistrict.Visibility;  //FoundDistrict.Visibility =  FoundDistrict.Visibility ? false : true;
        FoundDistrict.CityId = UpdatedDistrict.CityId;
        await _BaseDBContext.SaveChangesAsync();
        return UpdatedDistrict;
    }

    public async Task<IEnumerable<District>> GetAllDistrictsByCityName(string CityName)
    {
        return await _BaseDBContext.Districts.Where(x=>x.City.Name == CityName).ToListAsync();
    }

}