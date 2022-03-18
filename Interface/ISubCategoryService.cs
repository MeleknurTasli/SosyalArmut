using Armut.Model;
public interface ISubCategoryService
{
    public Task<IEnumerable<SubCategory>> GetAllSubCategories();
    public Task<SubCategory> GetSubCategoryById(int Id);
    public Task<SubCategory> GetSubCategoryByName(string Name);
    public Task<IEnumerable<SubCategory>> GetAllSubCategoriesByCategoryName(string CategoryName);
    //public Task DeleteSubCategory(int Id);
    public Task ChangeVisibility(SubCategory SubCategory);
    public Task<ActionResult<SubCategory>> CreateSubCategory(SubCategory SubCategory);
    public Task<ActionResult<SubCategory>> UpdateSubCategory(SubCategory SubCategory);

}