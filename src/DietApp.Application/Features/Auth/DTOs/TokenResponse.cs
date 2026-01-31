namespace DietApp.Application.Features.Auth.DTOs;

public sealed record TokenResponse(
    string AccessToken,
    string RefreshToken,
    DateTime AccessTokenExpiry
);
