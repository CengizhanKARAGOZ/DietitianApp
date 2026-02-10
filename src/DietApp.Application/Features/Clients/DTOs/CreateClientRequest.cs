using DietApp.Domain.Enums;

namespace DietApp.Application.Features.Clients.DTOs;

public sealed record CreateClientRequest(
    string FirstName,
    string LastName,
    string? Email,
    string? Phone,
    Gender Gender = Gender.NotSpecified,
    int? BirthYear = null,
    int? BirthMonth = null,
    decimal? Height = null,
    decimal? TargetWeight = null,
    string? GoalDescription = null,
    string? Allergies = null,
    string? HealthNotes = null,
    string? Tags = null,
    string? Notes = null
);
