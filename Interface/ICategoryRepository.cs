using Armut.Model;

public interface ICategoryRepository
{
    public Task<IEnumerable<Category>> GetAllCategories();
    public Task<Category> FindCategoryById(int Id);
    public Task<Category> FindCategoryByName(string Name);
    public Task<Category> UpdateCategory(Category category);
    //public Task DeleteCategory(int Id);
    public Task<Category> CreateCategory(Category category);
    public Task ChangeVisibility(Category category);
}