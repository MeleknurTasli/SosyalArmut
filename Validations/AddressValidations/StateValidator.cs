public class StateValidator : AbstractValidator<State>
{
    BaseDBContext _db;
    public StateValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(2).WithMessage("Geçerli isim giriniz.");
        RuleFor(x => x.CountryId).NotNull().Must(IsCountryIdExist).WithMessage("Gecerli city Id giriniz.");
    } 
    private bool IsCountryIdExist(int Id)
    {
        return _db.Countries.Any(x => x.Id == Id);
    }
 

}