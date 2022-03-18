using Armut.Model;
public interface ISubCategoryRepository
{
    public Task<IEnumerable<SubCategory>> GetAllSubCategories();
    public Task<SubCategory> GetSubCategoryById(int Id);
    public Task<SubCategory> GetSubCategoryByName(string Name);
    public Task<IEnumerable<SubCategory>> GetAllSubCategoriesByCategoryName(string CategoryName);
    //public Task DeleteSubCategory(int Id);
    public Task ChangeVisibility(SubCategory SubCategory);
    public Task<SubCategory> CreateSubCategory(SubCategory SubCategory);
    public Task<SubCategory> UpdateSubCategory(SubCategory SubCategory);
    public bool IsIDValid(int? Id);

}