public class CityValidator : AbstractValidator<City>
{
    BaseDBContext _db;
    public CityValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(2).WithMessage("Geçerli isim giriniz.");
        RuleFor(x => x.StateId).Must(IsStateIdExist).WithMessage("Gecerli state Id giriniz.");
        RuleFor(x => x.CountryId).NotNull().Must(IsCountryIdExist).WithMessage("Gecerli city Id giriniz.");
    }
    private bool IsCountryIdExist(int Id)
    {
        return _db.Countries.Any(x => x.Id == Id);
    }
    private bool IsStateIdExist(int? Id)
    {
        if(Id == null) return true;
        return _db.States.Any(x => x.Id == Id);
    }
 

}