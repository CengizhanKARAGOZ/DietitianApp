using DietApp.Application.Common.Interfaces;
using DietApp.Application.Features.Auth.DTOs;
using DietApp.Domain.Enums;
using DietApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Features.Auth.Commands.RefreshToken;

public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, TokenResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IJwtService _jwtService;
    private readonly IDateTimeService _dateTime;

    public RefreshTokenCommandHandler(
        IApplicationDbContext context,
        IJwtService jwtService,
        IDateTimeService dateTime)
    {
        _context = context;
        _jwtService = jwtService;
        _dateTime = dateTime;
    }

    public async Task<TokenResponse> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var existingToken = await _context.RefreshTokens
            .Include(rt => rt.User)
            .ThenInclude(u => u.Tenant)
            .FirstOrDefaultAsync(rt => rt.Token == request.RefreshToken, cancellationToken);

        if (existingToken == null)
        {
            throw new UnauthorizedException("Geçersiz refresh token.");
        }

        if (!existingToken.IsActive)
        {
            throw new UnauthorizedException("Refresh token geçersiz veya süresi dolmuş.");
        }

        var user = existingToken.User;

        if (user.IsDeleted || user.Status != UserStatus.Active)
        {
            throw new UnauthorizedException("Kullanıcı hesabı aktif değil.");
        }

        // Revoke old token
        existingToken.IsRevoked = true;
        existingToken.RevokedAt = _dateTime.UtcNow;
        existingToken.RevokedByIp = request.IpAddress;

        // Generate new tokens
        var newAccessToken = _jwtService.GenerateAccessToken(user);
        var newRefreshToken = _jwtService.GenerateRefreshToken();

        existingToken.ReplacedByToken = newRefreshToken;

        var refreshTokenEntity = new Domain.Entities.Identity.RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = newRefreshToken,
            ExpiresAt = _dateTime.UtcNow.AddDays(7),
            CreatedAt = _dateTime.UtcNow,
            CreatedByIp = request.IpAddress,
            UserAgent = request.UserAgent
        };

        _context.RefreshTokens.Add(refreshTokenEntity);
        await _context.SaveChangesAsync(cancellationToken);

        return new TokenResponse(
            AccessToken: newAccessToken,
            RefreshToken: newRefreshToken,
            AccessTokenExpiry: _dateTime.UtcNow.AddMinutes(15)
        );
    }
}
