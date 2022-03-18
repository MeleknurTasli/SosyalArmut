public class UpdateRankingDTOValidator : AbstractValidator<UpdateRankingDTO>
{
    public UpdateRankingDTOValidator()
    {
        RuleFor(x => x.Id).NotNull().WithMessage("Id giriniz.");
        RuleFor(x => x.Value).NotNull().NotEmpty().GreaterThanOrEqualTo(0).LessThanOrEqualTo(10).WithMessage("0 ile 10 arasında puan verebilirsiniz.");
    }
}