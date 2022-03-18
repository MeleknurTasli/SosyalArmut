public class SubCategoryRepository : ISubCategoryRepository
{
    private readonly BaseDBContext _BaseDBContext;
    public SubCategoryRepository(BaseDBContext BaseDBContext)
    {
        _BaseDBContext = BaseDBContext;
    }

    public async Task<SubCategory> CreateSubCategory(SubCategory SubCategory)
    {
        await _BaseDBContext.SubCategories.AddAsync(SubCategory);
        await _BaseDBContext.SaveChangesAsync();
        return SubCategory;
    }

/*
    public async Task DeleteSubCategory(int Id)
    {
        
        SubCategory SubCategory = await GetSubCategoryById(Id);
        _BaseDBContext.SubCategories.Remove(SubCategory);
        await _BaseDBContext.SaveChangesAsync();
    }
*/

    public async Task<IEnumerable<SubCategory>> GetAllSubCategories()
    {
        List<int> categoryIds = await (from x in _BaseDBContext.Categories select x.Id).ToListAsync();
        List<SubCategory> SubCategories = new List<SubCategory>();
        foreach (SubCategory item in _BaseDBContext.SubCategories.ToList())
        {
            if (categoryIds.Contains(item.CategoryId))
            {
                SubCategories.Add(item);
            }
        }
        return SubCategories;
    }

    public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesByCategoryName(string CategoryName)
    {
        List<int> categoryIds = await (from x in _BaseDBContext.Categories select x.Id).ToListAsync();
        List<SubCategory> SubCategories = new List<SubCategory>();
        foreach (SubCategory item in await _BaseDBContext.SubCategories.Where(e=>e.Category.Name == CategoryName).ToListAsync())
        {
            if (categoryIds.Contains(item.CategoryId))
            {
                SubCategories.Add(item);
            }
        }
        return SubCategories;
    }

    public async Task<SubCategory> GetSubCategoryById(int Id)
    {
        return await _BaseDBContext.SubCategories.FirstOrDefaultAsync(e=>e.Id == Id);
    }

    public async Task<SubCategory> GetSubCategoryByName(string Name)
    {
        return await _BaseDBContext.SubCategories.FirstOrDefaultAsync(e=>e.Name == Name);
    }

    public async Task<SubCategory> UpdateSubCategory(SubCategory UpdatedSubCategory)
    {
        SubCategory FoundSubCategory = await _BaseDBContext.SubCategories.FindAsync(UpdatedSubCategory.Id);
        FoundSubCategory.Name = UpdatedSubCategory.Name;
        FoundSubCategory.CategoryId = UpdatedSubCategory.CategoryId;
        FoundSubCategory.Visibility = UpdatedSubCategory.Visibility;
        await _BaseDBContext.SaveChangesAsync();
        return UpdatedSubCategory;
    }

    public bool IsIDValid(int? Id)
    {
        if (_BaseDBContext.Categories.FirstOrDefault(x => x.Id == Id) != null)
        {
            return true;
        }
        return false;
    }

    public async Task ChangeVisibility(SubCategory SubCategory)
    {
        SubCategory FoundSubCategory = await _BaseDBContext.SubCategories.FindAsync(SubCategory.Id);
        if(FoundSubCategory.Visibility == false) FoundSubCategory.Visibility = true;
        else FoundSubCategory.Visibility = false;
        await _BaseDBContext.SaveChangesAsync();
    }
}