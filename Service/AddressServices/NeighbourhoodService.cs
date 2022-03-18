public class NeighbourhoodService : INeighbourhoodService
{
    private readonly INeighbourhoodRepository _INeighbourhoodRepository;
    public NeighbourhoodService(INeighbourhoodRepository _INeighbourhoodRepository)
    {
        this._INeighbourhoodRepository = _INeighbourhoodRepository;
    }

    public async Task<Neighbourhood> CreateNeighbourhood(Neighbourhood neighbourhood)
    {
        try
        {
            Neighbourhood _neighbourhood = await _INeighbourhoodRepository.GetNeighbourhoodById(neighbourhood.Id);
            if(_neighbourhood == null)
            {
                return await _INeighbourhoodRepository.CreateNeighbourhood(neighbourhood);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }

    public async Task DeleteNeighbourhood(int Id)
    {
        try
        {
            Neighbourhood _neighbourhood = await _INeighbourhoodRepository.GetNeighbourhoodById(Id);
            if (_neighbourhood != null)
            {
                await _INeighbourhoodRepository.DeleteNeighbourhood(Id);
            }

        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Neighbourhood>> GetAllNeighbourhoods()
    {
        try
        {
            return await _INeighbourhoodRepository.GetAllNeighbourhoods();
        }
        catch
        {
            throw;
        }
    }

    public async Task<IEnumerable<Neighbourhood>> GetAllNeighbourhoodsByDistrictName(string DistrictName)
    {
        try
        {
            return await _INeighbourhoodRepository.GetAllNeighbourhoodsByDistrictName(DistrictName);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Neighbourhood> GetNeighbourhoodById(int Id)
    {
        try
        {
            return await _INeighbourhoodRepository.GetNeighbourhoodById(Id);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Neighbourhood> GetNeighbourhoodByName(string Name)
    {
        try
        {
            return await _INeighbourhoodRepository.GetNeighbourhoodByName(Name);
        }
        catch
        {
            throw;
        }
    }

    public async Task<Neighbourhood> UpdateNeighbourhood(Neighbourhood neighbourhood)
    {
        try
        {
            Neighbourhood _neighbourhood = await _INeighbourhoodRepository.GetNeighbourhoodById(neighbourhood.Id);
            if (_neighbourhood != null)
            {
                return await _INeighbourhoodRepository.UpdateNeighbourhood(neighbourhood);
            }
            return null;
        }
        catch
        {
            throw;
        }
    }
}