using DietApp.Domain.Enums;

namespace DietApp.Application.Features.Clients.DTOs;

public sealed record ClientDto(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    string? Email,
    string? Phone,
    Gender Gender,
    int? BirthYear,
    int? BirthMonth,
    int? Age,
    decimal? Height,
    decimal? TargetWeight,
    string? GoalDescription,
    string? Allergies,
    string? HealthNotes,
    string? Tags,
    ClientStatus Status,
    string? Notes,
    DateTime? FirstConsultationDate,
    DateTime? LastConsultationDate,
    DateTime CreatedAt
);
