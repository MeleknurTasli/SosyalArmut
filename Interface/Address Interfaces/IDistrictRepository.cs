public interface IDistrictRepository
{
    public Task<District> GetDistrictById(int Id);
    public Task<District> GetDistrictByName(string Name);
    public Task<IEnumerable<District>> GetAllDistricts();
    public Task<IEnumerable<District>> GetAllDistrictsByCityName(string CityName);
    public Task<District> CreateDistrict(District district);
    public Task DeleteDistrict(int Id);
    public Task<District> UpdateDistrict(District district);
}