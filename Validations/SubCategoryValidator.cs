public class SubCategoryValidator : AbstractValidator<SubCategory>
{
    BaseDBContext db;
    public SubCategoryValidator(BaseDBContext db)
    {
        this.db = db;
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(20).WithMessage("Gecerli bir alt kategori adi giriniz.");
        RuleFor(x=>x.CategoryId).NotNull().Must(IsCategoryIdExist).WithMessage("Category Id mevcut değildir.");
    }
    private bool IsCategoryIdExist(int Id)
    {
        if (db.Categories.Any(x => x.Id == Id)) return true;
        return false;
    }
}