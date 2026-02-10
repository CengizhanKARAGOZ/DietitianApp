using DietApp.Application.Common.Interfaces;
using DietApp.Application.Features.Clients.DTOs;
using DietApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Features.Clients.Queries.GetClientById;

public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, ClientDto>
{
    private readonly IApplicationDbContext _context;

    public GetClientByIdQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ClientDto> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.ClientId && c.TenantId == request.TenantId, cancellationToken);

        if (client == null)
        {
            throw new NotFoundException("Danışan", request.ClientId);
        }

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
