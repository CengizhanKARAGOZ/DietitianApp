using DietApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Features.Auth.Commands.ForgotPassword;

public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTime;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(
        IApplicationDbContext context,
        IDateTimeService dateTime,
        IEmailService emailService)
    {
        _context = context;
        _dateTime = dateTime;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == request.Email && !u.IsDeleted, cancellationToken);

        // Always return success to prevent email enumeration
        if (user == null)
        {
            return Unit.Value;
        }

        user.PasswordResetToken = Guid.NewGuid().ToString("N");
        user.PasswordResetTokenExpiry = _dateTime.UtcNow.AddHours(1);
        user.UpdatedAt = _dateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        var resetLink = $"/reset-password?token={user.PasswordResetToken}";
        await _emailService.SendPasswordResetAsync(
            user.Email,
            resetLink,
            user.PreferredLanguage,
            cancellationToken);

        return Unit.Value;
    }
}
