namespace DietApp.Application.Features.Auth.DTOs;

public sealed record AuthResponse(
    Guid UserId,
    string Email,
    string FirstName,
    string LastName,
    string Role,
    Guid? TenantId,
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiry
);
