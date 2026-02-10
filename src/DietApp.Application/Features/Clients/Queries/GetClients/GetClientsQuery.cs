using DietApp.Application.Common.Behaviors;
using DietApp.Application.Common.Models;
using DietApp.Application.Features.Clients.DTOs;
using DietApp.Domain.Enums;
using MediatR;

namespace DietApp.Application.Features.Clients.Queries.GetClients;

public sealed class GetClientsQuery : PaginatedRequest, IRequest<PaginatedList<ClientListDto>>, ITenantRequest
{
    public Guid? TenantId { get; set; }
    public ClientStatus? Status { get; init; }
    public string? SearchTerm { get; init; }
}
