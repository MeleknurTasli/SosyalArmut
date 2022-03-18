public class CreateUpdateActivityTimeTableDTOValidator : AbstractValidator<CreateUpdateActivityTimeTableDTO>
{
    BaseDBContext _db;
    public CreateUpdateActivityTimeTableDTOValidator(BaseDBContext db)
    {
        _db = db;
        RuleFor(x => x.Quota).NotNull().NotEmpty().GreaterThanOrEqualTo(1).WithMessage("Kota en az 1 olmalıdır.");
        RuleFor(x => x.StartDate).NotNull().NotEmpty().GreaterThan(DateTime.Now).WithMessage("Başlangıç tarihi bu günden sonra olmalıdır.");
        RuleFor(x => x.EndDate).NotNull().NotEmpty().GreaterThan(x=>x.StartDate).WithMessage("Bitiş tarihi başlangıçtan sonra olmalıdır.");
        RuleFor(x => x.ActivityId).NotNull().Must(IsActivityIdExist).WithMessage("Aktivite Id bulunamadı.");
    }

    private bool IsActivityIdExist(int Id)
    {
        if (_db.Activities.Any(x => x.Id == Id)) return true;
        return false;
    }
}