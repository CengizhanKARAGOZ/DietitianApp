using FluentValidation;

namespace DietApp.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandValidator : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email gereklidir.")
            .EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");
    }
}
