using MediatR;

namespace DietApp.Application.Features.Auth.Commands.ChangePassword;

public sealed record ChangePasswordCommand(
    string CurrentPassword,
    string NewPassword
) : IRequest<Unit>;
