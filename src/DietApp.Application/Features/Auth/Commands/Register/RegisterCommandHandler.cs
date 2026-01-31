using DietApp.Application.Common.Interfaces;
using DietApp.Application.Features.Auth.DTOs;
using DietApp.Domain.Entities.Identity;
using DietApp.Domain.Enums;
using DietApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RefreshTokenEntity = DietApp.Domain.Entities.Identity.RefreshToken;
namespace DietApp.Application.Features.Auth.Commands.Register;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly IDateTimeService _dateTime;
    private readonly IEmailService _emailService;

    public RegisterCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IJwtService jwtService,
        IDateTimeService dateTime,
        IEmailService emailService)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _dateTime = dateTime;
        _emailService = emailService;
    }

    public async Task<AuthResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        // Check if email already exists
        bool emailExists = await _context.Users
            .AnyAsync(u => u.Email == request.Email && !u.IsDeleted, cancellationToken);

        if (emailExists)
        {
            throw new BusinessRuleException("Bu e-posta adresi zaten kayıtlı.", "EMAIL_EXISTS");
        }

        // Create tenant
        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            BusinessName = request.BusinessName,
            TaxNumber = request.TaxNumber,
            City = request.City,
            IsActive = true,
            CreatedAt = _dateTime.UtcNow
        };

        // Create user
        var user = new User
        {
            Id = Guid.NewGuid(),
            TenantId = tenant.Id,
            Email = request.Email,
            PasswordHash = _passwordHasher.Hash(request.Password),
            FirstName = request.FirstName,
            LastName = request.LastName,
            Phone = request.Phone,
            Role = UserRole.DietitianOwner,
            Status = UserStatus.Active,
            EmailConfirmed = false,
            EmailConfirmationToken = Guid.NewGuid().ToString("N"),
            EmailConfirmationTokenExpiry = _dateTime.UtcNow.AddHours(24),
            CreatedAt = _dateTime.UtcNow
        };

        // Create default brand settings
        var brandSettings = new TenantBrandSettings
        {
            Id = Guid.NewGuid(),
            TenantId = tenant.Id,
            CreatedAt = _dateTime.UtcNow
        };

        _context.Tenants.Add(tenant);
        _context.Users.Add(user);
        _context.TenantBrandSettings.Add(brandSettings);

        await _context.SaveChangesAsync(cancellationToken);

        // Generate tokens
        var accessToken = _jwtService.GenerateAccessToken(user);
        var refreshToken = _jwtService.GenerateRefreshToken();

        var refreshTokenEntity = new RefreshTokenEntity
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = refreshToken,
            ExpiresAt = _dateTime.UtcNow.AddDays(7),
            CreatedAt = _dateTime.UtcNow
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
}
