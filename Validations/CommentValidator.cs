public class CommentValidator : AbstractValidator<Comment>
{
    BaseDBContext _db;
    public CommentValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description null veya boş olmamalı.");
        RuleFor(x => x.ActivityId).NotNull().Must(IsActivityIdExist).WithMessage("Aktivite Id bulunamadı.");
        RuleFor(x => x.UserId).NotNull().Must(IsUserIdExist).WithMessage("User Id bulunamadı.");
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