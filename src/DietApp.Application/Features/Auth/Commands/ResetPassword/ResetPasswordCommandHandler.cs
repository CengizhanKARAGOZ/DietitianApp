using DietApp.Application.Common.Interfaces;
using DietApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Features.Auth.Commands.ResetPassword;

public class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeService _dateTime;

    public ResetPasswordCommandHandler(
        IApplicationDbContext context,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTime)
    {
        _context = context;
        _passwordHasher = passwordHasher;
        _dateTime = dateTime;
    }

    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u =>
                    u.PasswordResetToken == request.Token &&
                    !u.IsDeleted,
                cancellationToken);

        if (user == null)
        {
            throw new BusinessRuleException("Geçersiz veya süresi dolmuş token.", "INVALID_TOKEN");
        }

        if (!user.PasswordResetTokenExpiry.HasValue || user.PasswordResetTokenExpiry.Value < _dateTime.UtcNow)
        {
            throw new BusinessRuleException("Token süresi dolmuş. Lütfen tekrar şifre sıfırlama talep edin.", "TOKEN_EXPIRED");
        }

        user.PasswordHash = _passwordHasher.Hash(request.NewPassword);
        user.PasswordResetToken = null;
        user.PasswordResetTokenExpiry = null;
        user.FailedLoginAttempts = 0;
        user.LockoutEnd = null;
        user.UpdatedAt = _dateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
