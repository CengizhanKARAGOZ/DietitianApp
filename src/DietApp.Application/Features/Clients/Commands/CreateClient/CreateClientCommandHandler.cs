using DietApp.Application.Common.Interfaces;
using DietApp.Application.Features.Clients.DTOs;
using DietApp.Domain.Entities.Clients;
using DietApp.Domain.Enums;
using MediatR;

namespace DietApp.Application.Features.Clients.Commands.CreateClient;

public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, ClientDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTime;

    public CreateClientCommandHandler(IApplicationDbContext context, IDateTimeService dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<ClientDto> Handle(CreateClientCommand request, CancellationToken cancellationToken)
    {
        var client = new Client
        {
            Id = Guid.NewGuid(),
            TenantId = request.TenantId!.Value,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email,
            Phone = request.Phone,
            Gender = request.Gender,
            BirthYear = request.BirthYear,
            BirthMonth = request.BirthMonth,
            Height = request.Height,
            TargetWeight = request.TargetWeight,
            GoalDescription = request.GoalDescription,
            Allergies = request.Allergies,
            HealthNotes = request.HealthNotes,
            Tags = request.Tags,
            Notes = request.Notes,
            Status = ClientStatus.Active,
            FirstConsultationDate = _dateTime.UtcNow,
            CreatedAt = _dateTime.UtcNow
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync(cancellationToken);

        return new ClientDto(
            Id: client.Id,
            FirstName: client.FirstName,
            LastName: client.LastName,
            FullName: client.FullName,
            Email: client.Email,
            Phone: client.Phone,
            Gender: client.Gender,
            BirthYear: client.BirthYear,
            BirthMonth: client.BirthMonth,
            Age: client.Age,
            Height: client.Height,
            TargetWeight: client.TargetWeight,
            GoalDescription: client.GoalDescription,
            Allergies: client.Allergies,
            HealthNotes: client.HealthNotes,
            Tags: client.Tags,
            Status: client.Status,
            Notes: client.Notes,
            FirstConsultationDate: client.FirstConsultationDate,
            LastConsultationDate: client.LastConsultationDate,
            CreatedAt: client.CreatedAt
        );
    }
}
