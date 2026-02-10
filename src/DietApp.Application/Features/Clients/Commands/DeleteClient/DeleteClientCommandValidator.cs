using FluentValidation;

namespace DietApp.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandValidator : AbstractValidator<DeleteClientCommand>
{
    public DeleteClientCommandValidator()
    {
        RuleFor(x => x.ClientId)
            .NotEmpty().WithMessage("Danışan ID gereklidir.");
    }
}
