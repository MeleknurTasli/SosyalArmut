using Armut.Model;

public interface ICategoryService
{
    public Task<IEnumerable<Category>> GetAllCategories();
    public Task<Category> FindCategoryById(int Id);
    public Task<Category> FindCategoryByName(string Name);
    public Task<ActionResult<Category>> UpdateCategory(Category category);
    //public Task DeleteCategory(int Id);
    public Task<ActionResult<Category>> CreateCategory(Category category);
    public Task ChangeVisibility(Category category);
}