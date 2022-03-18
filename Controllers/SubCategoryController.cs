namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class SubCategoryController : ControllerBase
{
    private readonly ISubCategoryService _SubCategoryService;
    public SubCategoryController(ISubCategoryService SubCategoryService)
    {
        _SubCategoryService = SubCategoryService;
    }

    [HttpPost]
    public async Task<ActionResult<SubCategory>> CreateSubCategory(SubCategory SubCategory)
    {
        return await _SubCategoryService.CreateSubCategory(SubCategory);
    }

    [HttpGet]
    public async Task<IEnumerable<SubCategory>> GetAllSubCategories()
    {
        return await _SubCategoryService.GetAllSubCategories();
    }

    [HttpGet("CategoryName")]
    public async Task<IEnumerable<SubCategory>> GetAllSubCategoriesByCategoryName(string CategoryName)
    {
        return await _SubCategoryService.GetAllSubCategoriesByCategoryName(CategoryName);
    }

    [HttpGet("{Id}")]
    public async Task<SubCategory> GetSubCategoryById(int Id)
    {
        return await _SubCategoryService.GetSubCategoryById(Id);
    }

    [HttpGet("Name")]
    public async Task<SubCategory> GetSubCategoryByName(string Name)
    {
        return await _SubCategoryService.GetSubCategoryByName(Name);
    }

    [HttpPut]
    public async Task<ActionResult<SubCategory>> UpdateSubCategory(SubCategory SubCategory)
    {
        return await _SubCategoryService.UpdateSubCategory(SubCategory);
    }

}