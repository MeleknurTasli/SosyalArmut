public class DistrictService : IDistrictService
{
    private readonly IDistrictRepository _IDistrictRepository;
    public DistrictService(IDistrictRepository _IDistrictRepository)
    {
        this._IDistrictRepository = _IDistrictRepository;
    }

    public async Task<District> CreateDistrict(District District)
    {
        try
        {
            District _District = await _IDistrictRepository.GetDistrictById(District.Id);
            if(_District == null)
            {
                return await _IDistrictRepository.CreateDistrict(District);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteDistrict(int Id)
    {
        try
        {
            District _District = await _IDistrictRepository.GetDistrictById(Id);
            if (_District != null)
            {
                await _IDistrictRepository.DeleteDistrict(Id);
            }

        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<District>> GetAllDistricts()
    {
        try
        {
            return await _IDistrictRepository.GetAllDistricts();
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<District>> GetAllDistrictsByCityName(string CityName)
    {
        try
        {
            return await _IDistrictRepository.GetAllDistrictsByCityName(CityName);
        }
        catch
        {
            throw;
        }
    }

    public async Task<District> GetDistrictById(int Id)
    {
        try
        {
            return await _IDistrictRepository.GetDistrictById(Id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<District> GetDistrictByName(string Name)
    {
        try
        {
            return await _IDistrictRepository.GetDistrictByName(Name);
        }
        catch
        {
            throw;
        }
    }

    public async Task<District> UpdateDistrict(District District)
    {
        try
        {
            District _District = await _IDistrictRepository.GetDistrictById(District.Id);
            if (_District != null)
            {
                return await _IDistrictRepository.UpdateDistrict(District);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }
}