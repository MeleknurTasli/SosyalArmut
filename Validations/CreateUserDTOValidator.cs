public class CreateUserDTOValidator : AbstractValidator<CreateUserDTO>
{
    BaseDBContext _db;
    public CreateUserDTOValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Username).NotNull().NotEmpty().Must(UniqueUserName).WithMessage("Bu username kullanılmaktadır. Başka bir username bulunuz.");
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(2).WithMessage("Geçerli bir ad giriniz.");
        RuleFor(x => x.LastName).NotNull().NotEmpty().MinimumLength(2).WithMessage("Geçerli bir soyad giriniz.");
        RuleFor(x => x.Age).GreaterThan(12).WithMessage("Yaşınız 12den büyük olmalı.");
        RuleFor(x => x.AccountId).NotNull().Must(IsAccountIdExist).Must(UniqueAccountID).WithMessage("Geçerli ya da farklı bir account id girin.");
        RuleFor(x => x.GenderId).NotNull().Must(IsGenderIdExist).WithMessage("Gender bulunamadı.");
        RuleFor(x => x.RoleIds).NotNull().NotEmpty().WithMessage("Role atanması yapılmalıdır.");
    }
    private bool UniqueUserName(string name)
    {
        var user = _db.Users
                            .Where(x => x.Username.ToLower() == name.ToLower())
                            .FirstOrDefault();

        if (user == null)
            return true;

        return false;
    }
    private bool UniqueAccountID(int ID)
    {
        var user = _db.Users
                            .Where(x => x.AccountId == ID)
                            .FirstOrDefault();

        if (user == null)
            return true;

        return false;
    }

    private bool IsGenderIdExist(int Id)
    {
        if (_db.Genders.Any(x => x.Id == Id)) return true;
        return false;
    }

    private bool IsAccountIdExist(int Id)
    {
        if (_db.Genders.Any(x => x.Id == Id)) return true;
        return false;
    }
}