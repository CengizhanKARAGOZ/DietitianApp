using DietApp.Domain.Enums;

namespace DietApp.Application.Features.Clients.DTOs;

public sealed record ClientListDto(
    Guid Id,
    string FirstName,
    string LastName,
    string FullName,
    string? Email,
    string? Phone,
    Gender Gender,
    int? Age,
    ClientStatus Status,
    DateTime? LastConsultationDate,
    DateTime CreatedAt
);
