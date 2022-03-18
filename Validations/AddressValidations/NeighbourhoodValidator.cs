public class NeighbourhoodValidator : AbstractValidator<Neighbourhood>
{
    BaseDBContext _db;
    public NeighbourhoodValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(2).WithMessage("Geçerli isim giriniz.");
        RuleFor(x => x.DistrictId).NotNull().Must(IsDistrictIdExist).WithMessage("Gecerli city Id giriniz.");
    }
    private bool IsDistrictIdExist(int Id)
    {
        return _db.Districts.Any(x => x.Id == Id);
    }
 

}