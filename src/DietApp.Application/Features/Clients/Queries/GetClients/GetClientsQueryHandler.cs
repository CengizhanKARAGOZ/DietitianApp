using DietApp.Application.Common.Interfaces;
using DietApp.Application.Common.Models;
using DietApp.Application.Features.Clients.DTOs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Features.Clients.Queries.GetClients;

public class GetClientsQueryHandler : IRequestHandler<GetClientsQuery, PaginatedList<ClientListDto>>
{
    private readonly IApplicationDbContext _context;

    public GetClientsQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<ClientListDto>> Handle(GetClientsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Clients
            .AsNoTracking()
            .Where(c => c.TenantId == request.TenantId);

        // Status filter
        if (request.Status.HasValue)
        {
            query = query.Where(c => c.Status == request.Status.Value);
        }

        // Search filter using EF.Functions.Like for case-insensitive search
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            var pattern = $"%{request.SearchTerm}%";
            query = query.Where(c =>
                EF.Functions.Like(c.FirstName, pattern) ||
                EF.Functions.Like(c.LastName, pattern) ||
                (c.Email != null && EF.Functions.Like(c.Email, pattern)) ||
                (c.Phone != null && EF.Functions.Like(c.Phone, pattern)));
        }

        // Sorting
        query = request.SortBy?.ToUpperInvariant() switch
        {
            "FIRSTNAME" => request.SortDescending ? query.OrderByDescending(c => c.FirstName) : query.OrderBy(c => c.FirstName),
            "LASTNAME" => request.SortDescending ? query.OrderByDescending(c => c.LastName) : query.OrderBy(c => c.LastName),
            "EMAIL" => request.SortDescending ? query.OrderByDescending(c => c.Email) : query.OrderBy(c => c.Email),
            "STATUS" => request.SortDescending ? query.OrderByDescending(c => c.Status) : query.OrderBy(c => c.Status),
            "LASTCONSULTATIONDATE" => request.SortDescending ? query.OrderByDescending(c => c.LastConsultationDate) : query.OrderBy(c => c.LastConsultationDate),
            _ => request.SortDescending ? query.OrderByDescending(c => c.CreatedAt) : query.OrderBy(c => c.CreatedAt)
        };

        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(c => new ClientListDto(
                c.Id,
                c.FirstName,
                c.LastName,
                c.FirstName + " " + c.LastName,
                c.Email,
                c.Phone,
                c.Gender,
                c.BirthYear.HasValue ? DateTime.Today.Year - c.BirthYear.Value : null,
                c.Status,
                c.LastConsultationDate,
                c.CreatedAt
            ))
            .ToListAsync(cancellationToken);

        return new PaginatedList<ClientListDto>(items, totalCount, request.PageNumber, request.PageSize);
    }
}
