public class UpdateUserDTOValidator : AbstractValidator<UpdateUserDTO>
{
    BaseDBContext _db;
    public UpdateUserDTOValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Username).NotNull().NotEmpty().Must(UniqueUserName).WithMessage("Bu username kullanılmaktadır. Başka bir username bulunuz.");
        RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(2).WithMessage("Geçerli bir ad giriniz.");
        RuleFor(x => x.LastName).NotNull().NotEmpty().MinimumLength(2).WithMessage("Geçerli bir soyad giriniz.");
        RuleFor(x => x.Age).GreaterThan(12).WithMessage("Yaşınız 12den büyük olmalı.");
        RuleFor(x => x.GenderId).NotNull().Must(IsGenderIdExist).WithMessage("Gender bulunamadı.");
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

    private bool IsGenderIdExist(int Id)
    {
        if (_db.Genders.Any(x => x.Id == Id)) return true;
        return false;
    }
}