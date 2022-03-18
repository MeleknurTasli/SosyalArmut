public class NeighbourhoodRepository : INeighbourhoodRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public NeighbourhoodRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public async Task<IEnumerable<Neighbourhood>> GetAllNeighbourhoodsByDistrictName(string DistrictName)
    {
        return await _BaseDBContext.Neighbourhoods.Where(x=>x.District.Name == DistrictName).ToListAsync();
    }

    public async Task<Neighbourhood> CreateNeighbourhood(Neighbourhood Neighbourhood)
    {
        await _BaseDBContext.Neighbourhoods.AddAsync(Neighbourhood);
        await _BaseDBContext.SaveChangesAsync();
        return Neighbourhood;
    }

    public async Task DeleteNeighbourhood(int Id)
    {
        Neighbourhood Neighbourhood = await GetNeighbourhoodById(Id);
        _BaseDBContext.Neighbourhoods.Remove(Neighbourhood);
        await _BaseDBContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Neighbourhood>> GetAllNeighbourhoods()
    {
         return await _BaseDBContext.Neighbourhoods.ToListAsync();
    }

    public async Task<Neighbourhood> GetNeighbourhoodById(int Id)
    {
         return await _BaseDBContext.Neighbourhoods.FirstOrDefaultAsync(e=>e.Id == Id);
    }

    public async Task<Neighbourhood> GetNeighbourhoodByName(string Name)
    {
         return await _BaseDBContext.Neighbourhoods.FirstOrDefaultAsync(e=>e.Name == Name);
    }

    public async Task<Neighbourhood> UpdateNeighbourhood(Neighbourhood UpdatedNeighbourhood)
    {
        Neighbourhood FoundNeighbourhood = await _BaseDBContext.Neighbourhoods.FindAsync(UpdatedNeighbourhood.Id);
        FoundNeighbourhood.Name = UpdatedNeighbourhood.Name;
        FoundNeighbourhood.Visibility = UpdatedNeighbourhood.Visibility;
        FoundNeighbourhood.DistrictId = UpdatedNeighbourhood.DistrictId;
        await _BaseDBContext.SaveChangesAsync();
        return UpdatedNeighbourhood;
    }
}