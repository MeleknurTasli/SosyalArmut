namespace Armut.Controllers;

[ApiController]
[Route("[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _CategoryService;
    public CategoryController(ICategoryService CategoryService)
    {
        _CategoryService = CategoryService;
    }

    [HttpGet]
    public async Task<IEnumerable<Category>> GetAllCategories()
    {
        return await _CategoryService.GetAllCategories();
    }

    [HttpGet("{Id}")]
    public async Task<Category> FindCategoryById(int Id)
    {
        return await _CategoryService.FindCategoryById(Id);
    }

    [HttpGet("Name")] //https://localhost:7293/Category/name?name=Dans
    public async Task<Category> FindCategoryByName(string Name)
    {
        return await _CategoryService.FindCategoryByName(Name);
    }

    [HttpPut]
    public async Task<ActionResult<Category>> UpdateCategory(Category category)
    {
        return await _CategoryService.UpdateCategory(category);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category category)
    {
        return await _CategoryService.CreateCategory(category);
    }
}
