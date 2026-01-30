using DietApp.Application.Common.Interfaces;
using DietApp.Domain.Exceptions;
using MediatR;

namespace DietApp.Application.Common.Behaviors;

public interface ITenantRequest
{
    Guid? TenantId { get; set; }
}

public class TenantBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ITenantRequest
{
    private readonly ICurrentUserService _currentUserService;

    public TenantBehavior(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_currentUserService.IsSuperAdmin)
        {
            return await next();
        }

        if (!_currentUserService.TenantId.HasValue)
        {
            throw new UnauthorizedException("Tenant context is required.");
        }

        request.TenantId = _currentUserService.TenantId;

        return await next();
    }
}
