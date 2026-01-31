using DietApp.Application.Features.Auth.DTOs;
using MediatR;

namespace DietApp.Application.Features.Auth.Commands.Register;

public sealed record RegisterCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    string? Phone,
    string BusinessName,
    string? TaxNumber,
    string? City,
    string PrefferedLanguage = "tr-TR"
) : IRequest<AuthResponse>;
