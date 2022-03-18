using Armut.Model;

public class CategoryRepository : ICategoryRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public CategoryRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public async Task<Category> CreateCategory(Category category)
    {
        await _BaseDBContext.Categories.AddAsync(category);
        await _BaseDBContext.SaveChangesAsync();
        return category;
    }

/*
    public async Task DeleteCategory(int Id)
    {
        Category category = await FindCategoryById(Id);
        _BaseDBContext.Categories.Remove(category);
        await _BaseDBContext.SaveChangesAsync();
    }
*/

    public async Task ChangeVisibility(Category category)
    {
        Category FoundCategory = await _BaseDBContext.Categories.FindAsync(category.Id);
        if(FoundCategory.Visibility == false) FoundCategory.Visibility = true;
        else FoundCategory.Visibility = false;
        await _BaseDBContext.SaveChangesAsync();
    }

    public async Task<Category> FindCategoryById(int Id)
    {
        return await _BaseDBContext.Categories.FirstOrDefaultAsync(e=>e.Id == Id);
    }

    public async Task<Category> FindCategoryByName(string Name)
    {
        return await _BaseDBContext.Categories.FirstOrDefaultAsync(e=>e.Name == Name);
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _BaseDBContext.Categories.Where(x => x.Visibility == true).ToListAsync();
    }

    public async Task<Category> UpdateCategory(Category UpdatedCategory)
    {
        Category FoundCategory = await _BaseDBContext.Categories.FindAsync(UpdatedCategory.Id);
        FoundCategory.Name = UpdatedCategory.Name;
        FoundCategory.Visibility = UpdatedCategory.Visibility;
        await _BaseDBContext.SaveChangesAsync();
        return UpdatedCategory;
    }

    public bool IncidenceExist(int id)
    {
        return _BaseDBContext.Categories.Any(e => e.Id == id);
    }
}