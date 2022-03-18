public class CategoryValidator : AbstractValidator<Category>
{
    public CategoryValidator()
    {
        RuleFor(x => x.Name).MinimumLength(3).MaximumLength(20).WithMessage("Gecerli bir kategori adi giriniz.");
    }
}