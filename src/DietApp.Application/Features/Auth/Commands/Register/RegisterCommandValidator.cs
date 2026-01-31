using FluentValidation;

namespace DietApp.Application.Features.Auth.Commands.Register;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-posta adresi gereklidir.")
            .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
            .MaximumLength(256).WithMessage("E-posta adresi en fazla 256 karakter olabilir.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Şifre gereklidir.")
            .MinimumLength(8).WithMessage("Şifre en az 8 karakter olmalıdır.")
            .MaximumLength(128).WithMessage("Şifre en fazla 128 karakter olabilir.")
            .Matches("[A-Z]").WithMessage("Şifre en az bir büyük harf içermelidir.")
            .Matches("[a-z]").WithMessage("Şifre en az bir küçük harf içermelidir.")
            .Matches("[0-9]").WithMessage("Şifre en az bir rakam içermelidir.");

        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad gereklidir.")
            .MaximumLength(100).WithMessage("Ad en fazla 100 karakter olabilir.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad gereklidir.")
            .MaximumLength(100).WithMessage("Soyad en fazla 100 karakter olabilir.");

        When(x => !string.IsNullOrEmpty(x.Phone), () =>
        {
            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Telefon numarası en fazla 20 karakter olabilir.");
        });

        RuleFor(x => x.BusinessName)
            .NotEmpty().WithMessage("İşletme adı gereklidir.")
            .MaximumLength(200).WithMessage("İşletme adı en fazla 200 karakter olabilir.");

        When(x => !string.IsNullOrEmpty(x.TaxNumber), () =>
        {
            RuleFor(x => x.TaxNumber)
                .MaximumLength(20).WithMessage("Vergi numarası en fazla 20 karakter olabilir.");
        });

        When(x => !string.IsNullOrEmpty(x.City), () =>
        {
            RuleFor(x => x.City)
                .MaximumLength(100).WithMessage("Şehir en fazla 100 karakter olabilir.");
        });
    }
}
