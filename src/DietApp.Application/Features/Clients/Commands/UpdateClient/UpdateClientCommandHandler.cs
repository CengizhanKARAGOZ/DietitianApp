using DietApp.Application.Common.Interfaces;
using DietApp.Application.Features.Clients.DTOs;
using DietApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Features.Clients.Commands.UpdateClient;

public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, ClientDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IDateTimeService _dateTime;

    public UpdateClientCommandHandler(IApplicationDbContext context, IDateTimeService dateTime)
    {
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<ClientDto> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == request.ClientId && c.TenantId == request.TenantId, cancellationToken);

        if (client == null)
        {
            throw new NotFoundException("Danışan", request.ClientId);
        }

        client.FirstName = request.FirstName;
        client.LastName = request.LastName;
        client.Email = request.Email;
        client.Phone = request.Phone;
        client.Gender = request.Gender;
        client.BirthYear = request.BirthYear;
        client.BirthMonth = request.BirthMonth;
        client.Height = request.Height;
        client.TargetWeight = request.TargetWeight;
        client.GoalDescription = request.GoalDescription;
        client.Allergies = request.Allergies;
        client.HealthNotes = request.HealthNotes;
        client.Tags = request.Tags;
        client.Status = request.Status;
        client.Notes = request.Notes;
        client.UpdatedAt = _dateTime.UtcNow;

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
