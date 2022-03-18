public class CountryValidator : AbstractValidator<Country>
{
    BaseDBContext _db;
    public CountryValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(2).WithMessage("Geçerli isim giriniz.");
    }
 

}