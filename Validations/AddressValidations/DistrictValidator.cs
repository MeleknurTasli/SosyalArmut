public class DistrictValidator : AbstractValidator<District>
{
    BaseDBContext _db;
    public DistrictValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(2).WithMessage("Geçerli isim giriniz.");
        RuleFor(x => x.CityId).NotNull().Must(IsCityIdExist).WithMessage("Gecerli city Id giriniz.");
    }
    private bool IsCityIdExist(int Id)
    {
        return _db.Cities.Any(x => x.Id == Id);
    }
 

}