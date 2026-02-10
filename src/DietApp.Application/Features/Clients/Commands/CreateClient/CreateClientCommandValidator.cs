using FluentValidation;

namespace DietApp.Application.Features.Clients.Commands.CreateClient;

public class CreateClientCommandValidator : AbstractValidator<CreateClientCommand>
{
    public CreateClientCommandValidator()
    {
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("Ad gereklidir.")
            .MaximumLength(100).WithMessage("Ad en fazla 100 karakter olabilir.");

        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("Soyad gereklidir.")
            .MaximumLength(100).WithMessage("Soyad en fazla 100 karakter olabilir.");

        When(x => !string.IsNullOrEmpty(x.Email), () =>
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.")
                .MaximumLength(256).WithMessage("E-posta en fazla 256 karakter olabilir.");
        });

        When(x => !string.IsNullOrEmpty(x.Phone), () =>
        {
            RuleFor(x => x.Phone)
                .MaximumLength(20).WithMessage("Telefon numarası en fazla 20 karakter olabilir.");
        });

        When(x => x.BirthYear.HasValue, () =>
        {
            RuleFor(x => x.BirthYear)
                .InclusiveBetween(1900, DateTime.Now.Year).WithMessage("Geçerli bir doğum yılı giriniz.");
        });

        When(x => x.BirthMonth.HasValue, () =>
        {
            RuleFor(x => x.BirthMonth)
                .InclusiveBetween(1, 12).WithMessage("Geçerli bir ay giriniz (1-12).");
        });

        When(x => x.Height.HasValue, () =>
        {
            RuleFor(x => x.Height)
                .InclusiveBetween(50, 300).WithMessage("Boy 50-300 cm arasında olmalıdır.");
        });

        When(x => x.TargetWeight.HasValue, () =>
        {
            RuleFor(x => x.TargetWeight)
                .InclusiveBetween(20, 500).WithMessage("Hedef kilo 20-500 kg arasında olmalıdır.");
        });

        RuleFor(x => x.GoalDescription)
            .MaximumLength(500).WithMessage("Hedef açıklaması en fazla 500 karakter olabilir.");

        RuleFor(x => x.Allergies)
            .MaximumLength(1000).WithMessage("Alerji bilgisi en fazla 1000 karakter olabilir.");

        RuleFor(x => x.HealthNotes)
            .MaximumLength(2000).WithMessage("Sağlık notları en fazla 2000 karakter olabilir.");

        RuleFor(x => x.Tags)
            .MaximumLength(500).WithMessage("Etiketler en fazla 500 karakter olabilir.");

        RuleFor(x => x.Notes)
            .MaximumLength(2000).WithMessage("Notlar en fazla 2000 karakter olabilir.");
    }
}
