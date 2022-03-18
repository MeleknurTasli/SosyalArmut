public class AddressValidator : AbstractValidator<Address>
{
    BaseDBContext _db;
    public AddressValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.CityId).NotNull().Must(IsCityIdExist).WithMessage("Gecerli city Id giriniz.");
        RuleFor(x => x.StateId).Must(IsStateIdExist).WithMessage("Gecerli state Id giriniz.");
        RuleFor(x => x.NeighbourhoodId).NotNull().Must(IsNeighbourhoodIdExist).WithMessage("Gecerli city Id giriniz.");
        RuleFor(x => x.DistrictId).NotNull().Must(IsDistrictIdExist).WithMessage("Gecerli city Id giriniz.");
        RuleFor(x => x.CountryId).NotNull().Must(IsCountryIdExist).WithMessage("Gecerli city Id giriniz.");
    }
    private bool IsCityIdExist(int Id)
    {
        return _db.Cities.Any(x => x.Id == Id);
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
    private bool IsDistrictIdExist(int Id)
    {
        return _db.Districts.Any(x => x.Id == Id);
    }
    private bool IsNeighbourhoodIdExist(int Id)
    {
        return _db.Neighbourhoods.Any(x => x.Id == Id);
    }
 

}