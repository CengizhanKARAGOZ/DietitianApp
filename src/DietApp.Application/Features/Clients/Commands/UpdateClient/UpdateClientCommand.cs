using DietApp.Application.Common.Behaviors;
using DietApp.Application.Features.Clients.DTOs;
using DietApp.Domain.Enums;
using MediatR;

namespace DietApp.Application.Features.Clients.Commands.UpdateClient;

public sealed class UpdateClientCommand : IRequest<ClientDto>, ITenantRequest
{
    public Guid? TenantId { get; set; }
    public Guid ClientId { get; init; }
    public string FirstName { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string? Email { get; init; }
    public string? Phone { get; init; }
    public Gender Gender { get; init; }
    public int? BirthYear { get; init; }
    public int? BirthMonth { get; init; }
    public decimal? Height { get; init; }
    public decimal? TargetWeight { get; init; }
    public string? GoalDescription { get; init; }
    public string? Allergies { get; init; }
    public string? HealthNotes { get; init; }
    public string? Tags { get; init; }
    public ClientStatus Status { get; init; }
    public string? Notes { get; init; }
}
