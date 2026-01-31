using DietApp.Application.Common.Interfaces;
using DietApp.Application.Features.Auth.DTOs;
using DietApp.Domain.Entities.Identity;
using DietApp.Domain.Enums;
using DietApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefreshTokenEntity = DietApp.Domain.Entities.Identity.RefreshToken;
namespace DietApp.Application.Features.Auth.Commands.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IDateTimeService _dateTime;

    public LoginCommandHandler(IApplicationDbContext context, IPasswordHasher passwordHasher, IJwtService jwtService, IDateTimeService dateTime)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _dateTime = dateTime;
    }

    public async Task<AuthResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .Include(u => u.Tenant)
            .FirstOrDefaultAsync(u => u.Email == request.Email, cancellationToken);

        if (user == null)
        {
            throw new UnauthorizedException("Geçersiz e-posta veya şifre.");
        }

        if (user.IsLocked)
        {
            throw new UnauthorizedException($"Hesabınız kilitlendi. Lütfen {user.LockoutEnd:HH:mm} sonra tekrar deneyin.");
        }

        if (user.Status != UserStatus.Active)
        {
            throw new UnauthorizedException("Hesabınız aktif değil.");
        }

        if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        {
            await HandleFailedLoginAsync(user, cancellationToken);
        }

        // Reset failed login attempts on successful login
        user.FailedLoginAttempts = 0;
        user.LockoutEnd = null;
        user.LastLoginAt = _dateTime.UtcNow;

        // Generate tokens
        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        // Save refresh token
        var refreshTokenEntity = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = _dateTime.UtcNow.AddDays(7),
            CreatedAt = _dateTime.UtcNow,
            CreatedByIp = request.IpAddress,
            UserAgent = request.UserAgent
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return new AuthResponse(
            UserId: user.Id,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Role: user.Role.ToString(),
            TenantId: user.TenantId,
            AccessToken: accessToken,
            RefreshToken: refreshToken,
            AccessTokenExpiry: _dateTime.UtcNow.AddMinutes(15)
        );
    }

    private async Task HandleFailedLoginAsync(User user, CancellationToken cancellationToken)
    {
        user.FailedLoginAttempts++;

        if (user.FailedLoginAttempts >= 5)
        {
            user.LockoutEnd = _dateTime.UtcNow.AddMinutes(15);
            user.Status = UserStatus.Locked;
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}

