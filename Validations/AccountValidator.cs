public class AccountDTOValidator : AbstractValidator<LoginDTO>
{
    public AccountDTOValidator()
    {
        RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.Net4xRegex).WithMessage("Gecerli email giriniz.");
        RuleFor(x => x.Password).NotNull().MinimumLength(6).WithMessage("Şifre en az 6 karakter uzunluğunda olmalıdır.");
    }
}