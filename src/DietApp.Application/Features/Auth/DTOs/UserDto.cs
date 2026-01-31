using DietApp.Domain.Enums;

namespace DietApp.Application.Features.Auth.DTOs;

public sealed record UserDto(
    Guid Id,
    string Email,
    string FirstName,
    string LastName,
    string? Phone,
    UserRole Role,
    UserStatus Status,
    Guid? TenantId,
    bool EmailConfirmed,
    bool MfaEnabled,
    DateTime? LastLoginAt,
    DateTime CreatedAt
);
