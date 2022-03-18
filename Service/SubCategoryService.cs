public class SubCategoryService : ControllerBase, ISubCategoryService
{
    private readonly ISubCategoryRepository _SubCategoryRepository;
    public SubCategoryService(ISubCategoryRepository SubCategoryRepository)
    {
        _SubCategoryRepository = SubCategoryRepository;
    }

    public async Task ChangeVisibility(SubCategory SubCategory)
    {
        try
        {
            SubCategory SubFoundCategory = await _SubCategoryRepository.GetSubCategoryById(SubCategory.Id);
            if (SubFoundCategory != null)
            {
                await _SubCategoryRepository.ChangeVisibility(SubCategory);
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<ActionResult<SubCategory>> CreateSubCategory(SubCategory SubCategory)
    {
        try
        {
            SubCategory SubFoundCategory = await _SubCategoryRepository.GetSubCategoryById(SubCategory.Id);
            if (SubFoundCategory == null && _SubCategoryRepository.IsIDValid(SubCategory.CategoryId))
            {
                SubFoundCategory = await _SubCategoryRepository.GetSubCategoryByName(SubCategory.Name);
                if (SubFoundCategory == null)
                {
                    return await _SubCategoryRepository.CreateSubCategory(SubCategory);
                }
                else return BadRequest("Bu isimde bir alt kategori mevcuttur.");
            }
            else return BadRequest("Bu Id ile bir alt kategori mevcuttur veya girilen category Id mevcut değildir.");
        }
        catch
        {
            return null;
        }
    }

/*
    public async Task DeleteSubCategory(int Id)
    {
        try
        {
            SubCategory SubFoundCategory = await _SubCategoryRepository.GetSubCategoryById(Id);
            if (SubFoundCategory != null)
            {
                await _SubCategoryRepository.DeleteSubCategory(SubFoundCategory.Id);
            }
        }
        catch
        {
            throw;
        }
    }
*/

    public async Task<IEnumerable<SubCategory>> GetAllSubCategories()
    {
        try
        {
            return await _SubCategoryRepository.GetAllSubCategories();
        }
        catch
        {
            return null;
        }
    }

    public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesByCategoryName(string CategoryName)
    {
        try
        {
            return await _SubCategoryRepository.GetAllSubCategoriesByCategoryName(CategoryName);
        }
        catch
        {
            return null;
        }
    }

    public async Task<SubCategory> GetSubCategoryById(int Id)
    {
        try
        {
            return await _SubCategoryRepository.GetSubCategoryById(Id);
        }
        catch
        {
            return null;
        }
    }

    public async Task<SubCategory> GetSubCategoryByName(string Name)
    {
        try
        {
            return await _SubCategoryRepository.GetSubCategoryByName(Name);
        }
        catch
        {
            return null;
        }
    }

    public async Task<ActionResult<SubCategory>> UpdateSubCategory(SubCategory SubCategory)
    {
        try
        {
            SubCategory SubFoundCategory = await _SubCategoryRepository.GetSubCategoryById(SubCategory.Id);
            if (SubFoundCategory != null && _SubCategoryRepository.IsIDValid(SubCategory.CategoryId))
            {
                SubFoundCategory = await _SubCategoryRepository.GetSubCategoryByName(SubCategory.Name);
                if (SubFoundCategory == null)
                {
                    return await _SubCategoryRepository.UpdateSubCategory(SubCategory);
                }
                return  BadRequest("Bu isimde bir alt kategori mevcuttur.");
            }
            return  BadRequest("Bu Id ile bir alt kategori mevcut değildir veya girilen category Id mevcut değildir.");
        }
        catch
        {
            return null;
        }
    }
}