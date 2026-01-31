using DietApp.Application.Features.Auth.DTOs;
using MediatR;

namespace DietApp.Application.Features.Auth.Commands.RefreshToken;

public sealed record RefreshTokenCommand(
    string RefreshToken,
    string? IpAddress,
    string? UserAgent
) : IRequest<TokenResponse>;
