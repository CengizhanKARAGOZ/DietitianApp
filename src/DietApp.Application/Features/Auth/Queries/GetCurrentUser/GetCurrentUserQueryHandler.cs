using DietApp.Application.Common.Interfaces;
using DietApp.Application.Features.Auth.DTOs;
using DietApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Features.Auth.Queries.GetCurrentUser;

public class GetCurrentUserQueryHandler : IRequestHandler<GetCurrentUserQuery, UserDto>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public GetCurrentUserQueryHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<UserDto> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        if (!_currentUser.UserId.HasValue)
        {
            throw new UnauthorizedException();
        }

        var user = await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == _currentUser.UserId.Value && !u.IsDeleted, cancellationToken);

        if (user == null)
        {
            throw new NotFoundException("User", _currentUser.UserId.Value);
        }

        return new UserDto(
            Id: user.Id,
            Email: user.Email,
            FirstName: user.FirstName,
            LastName: user.LastName,
            Phone: user.Phone,
            Role: user.Role,
            Status: user.Status,
            TenantId: user.TenantId,
            EmailConfirmed: user.EmailConfirmed,
            MfaEnabled: user.MfaEnabled,
            LastLoginAt: user.LastLoginAt,
            CreatedAt: user.CreatedAt
        );
    }
}
