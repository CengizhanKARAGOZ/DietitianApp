using DietApp.Application.Common.Interfaces;
using DietApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Features.Auth.Commands.ChangePassword;

public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IDateTimeService _dateTime;

    public ChangePasswordCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser,
        IPasswordHasher passwordHasher,
        IDateTimeService dateTime)
    {
        _context = context;
        _currentUser = currentUser;
        _passwordHasher = passwordHasher;
        _dateTime = dateTime;
    }

    public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        if (!_currentUser.UserId.HasValue)
        {
            throw new UnauthorizedException();
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == _currentUser.UserId.Value && !u.IsDeleted, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User", _currentUser.UserId.Value);
        }

        if (!_passwordHasher.Verify(request.CurrentPassword, user.PasswordHash))
        {
            throw new BusinessRuleException("Mevcut şifre hatalı.", "INVALID_PASSWORD");
        }

        user.PasswordHash = _passwordHasher.Hash(request.NewPassword);
        user.UpdatedAt = _dateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
