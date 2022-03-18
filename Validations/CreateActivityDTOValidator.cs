public class CreateActivityDTOValidator : AbstractValidator<CreateActivityDTO>
{
    BaseDBContext _db;
    public CreateActivityDTOValidator(BaseDBContext _db)
    {
        this._db = _db;
        RuleFor(x => x.Name).MinimumLength(3).WithMessage("Aktivite ad uzunluğu 3 ile 50 karakter arasında sınırlandırılmıştır.");
        RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("Ücret 0 veya 0'dan büyük olmalıdır.");
        RuleFor(x=>x.AddressId).NotNull().Must(IsAddressIdExist).WithMessage("Address id mevcut değil");
        RuleFor(x=>x.OwnerUserId).NotNull().Must(IsUserIdExist).WithMessage("User id mevcut değil");
        RuleFor(x=>x.SubCategoryId).NotNull().Must(IsSubCategoryIdExist).WithMessage("Subcategory id mevcut değil");
    }
    private bool IsUserIdExist(int Id)
    {
        if (_db.Users.Any(x => x.Id == Id)) return true;
        return false;
    }
    private bool IsAddressIdExist(int Id)
    {
        if (_db.Addresses.Any(x => x.Id == Id)) return true;
        return false;
    }
    private bool IsSubCategoryIdExist(int Id)
    {
        if (_db.SubCategories.Any(x => x.Id == Id)) return true;
        return false;
    }

}