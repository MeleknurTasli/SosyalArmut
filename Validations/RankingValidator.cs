public class RankingValidator : AbstractValidator<Ranking>
{
    BaseDBContext _db;
    public RankingValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Value).NotNull().NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(10).WithMessage("0 ile 10 arasında puan verebilirsiniz.");
        RuleFor(x => x.ActivityId).NotNull().Must(IsActivityIdExist).WithMessage("Aktivite Id bulunamadı.");
        RuleFor(x => x.RatingUserId).NotNull().Must(IsUserIdExist).WithMessage("User Id bulunamadı.");
    }
    private bool IsUserIdExist(int Id)
    {
        if (_db.Users.Any(x => x.Id == Id)) return true;
        return false;
    }

    private bool IsActivityIdExist(int Id)
    {
        if (_db.Activities.Any(x => x.Id == Id)) return true;
        return false;
    }
}