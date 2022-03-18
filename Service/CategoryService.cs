using Armut.Model;

public class CategoryService : ControllerBase, ICategoryService
{
    private readonly ICategoryRepository _CategoryRepository;

    public CategoryService(ICategoryRepository CategoryRepository)
    {
        _CategoryRepository = CategoryRepository;
    }

    public async Task ChangeVisibility(Category category)
    {
         try
            {
                Category FoundCategory = await _CategoryRepository.FindCategoryById(category.Id);
                if (FoundCategory != null)
                {
                    await _CategoryRepository.ChangeVisibility(category);
                }
                else BadRequest("Hata : Bu Id ile bir kategori mevcut değildir.");
            }
            catch
            {
                throw;
            }
    }

    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        try
        {
            Category FoundCategory = await _CategoryRepository.FindCategoryById(category.Id);
            if (FoundCategory == null)
            {
                FoundCategory = await _CategoryRepository.FindCategoryByName(category.Name);
                if (FoundCategory == null)
                {
                    return Ok(await _CategoryRepository.CreateCategory(category));
                }
                else return BadRequest("Hata : Bu isimde bir kategori mevcuttur.");
            }
            else return BadRequest("Hata : Bu Id ile bir kategori mevcuttur.");
        }
        catch
        {
            throw;
        }
    }

    /*
        public async Task DeleteCategory(int Id)
        {
            try
            {
                Category FoundCategory = await _CategoryRepository.FindCategoryById(Id);
                if (FoundCategory != null)
                {
                    await _CategoryRepository.DeleteCategory(FoundCategory.Id);
                }
            }
            catch
            {
                throw;
            }
        }
    */

    public async Task<Category> FindCategoryById(int Id)
    {
        try
        {
            Category FoundCategory = await _CategoryRepository.FindCategoryById(Id);
            if (FoundCategory != null)
            {
                return FoundCategory;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<Category> FindCategoryByName(string Name)
    {
        try
        {
            Category FoundCategory = await _CategoryRepository.FindCategoryByName(Name);
            if (FoundCategory != null)
            {
                return FoundCategory;
            }
            return null;
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        try
        {
            return await _CategoryRepository.GetAllCategories();
        }
        catch
        {
            return null;
        }

    }

    public async Task<ActionResult<Category>> UpdateCategory(Category category)
    {
        try
        {
            Category FoundCategory = await _CategoryRepository.FindCategoryById(category.Id);
            if (FoundCategory != null)
            {
                FoundCategory = await _CategoryRepository.FindCategoryByName(category.Name);
                if (FoundCategory == null)
                {
                    return await _CategoryRepository.UpdateCategory(category);
                }
                else return  BadRequest("Hata : Bu isimde bir kategori mevcuttur.");
            }
            else return  BadRequest("Hata : Bu Id ile bir kategori mevcut değildir.");
        }
        catch
        {
            return null;
        }
    }
}