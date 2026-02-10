using DietApp.Domain.Enums;

namespace DietApp.Application.Features.Clients.DTOs;

public sealed record UpdateClientRequest(
    string FirstName,
    string LastName,
    string? Email,
    string? Phone,
    Gender Gender,
    int? BirthYear,
    int? BirthMonth,
    decimal? Height,
    decimal? TargetWeight,
    string? GoalDescription,
    string? Allergies,
    string? HealthNotes,
    string? Tags,
    ClientStatus Status,
    string? Notes
);
