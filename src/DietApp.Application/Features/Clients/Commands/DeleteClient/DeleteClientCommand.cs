using DietApp.Application.Common.Behaviors;
using MediatR;

namespace DietApp.Application.Features.Clients.Commands.DeleteClient;

public sealed class DeleteClientCommand : IRequest<Unit>, ITenantRequest
{
    public Guid? TenantId { get; set; }
    public Guid ClientId { get; init; }
}
