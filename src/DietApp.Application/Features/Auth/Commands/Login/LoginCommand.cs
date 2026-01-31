using DietApp.Application.Features.Auth.DTOs;
using MediatR;

namespace DietApp.Application.Features.Auth.Commands.Login;

public sealed record LoginCommand(
    string Email,
    string Password,
    string? IpAddress,
    string? UserAgent
) : IRequest<AuthResponse>;
