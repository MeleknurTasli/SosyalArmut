public interface INeighbourhoodService
{
    public Task<Neighbourhood> GetNeighbourhoodById(int Id);
    public Task<Neighbourhood> GetNeighbourhoodByName(string Name);
    public Task<IEnumerable<Neighbourhood>> GetAllNeighbourhoods();
    public Task<IEnumerable<Neighbourhood>> GetAllNeighbourhoodsByDistrictName(string DistrictName);
    public Task<Neighbourhood> CreateNeighbourhood(Neighbourhood neighbourhood);
    public Task DeleteNeighbourhood(int Id);
    public Task<Neighbourhood> UpdateNeighbourhood(Neighbourhood neighbourhood);
}