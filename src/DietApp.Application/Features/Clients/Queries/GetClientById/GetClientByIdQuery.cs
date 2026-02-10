using DietApp.Application.Common.Behaviors;
using DietApp.Application.Features.Clients.DTOs;
using MediatR;

namespace DietApp.Application.Features.Clients.Queries.GetClientById;

public class GetClientByIdQuery : IRequest<ClientDto>, ITenantRequest
{
    public Guid? TenantId { get; set; }
    public Guid ClientId { get; init; }
}
