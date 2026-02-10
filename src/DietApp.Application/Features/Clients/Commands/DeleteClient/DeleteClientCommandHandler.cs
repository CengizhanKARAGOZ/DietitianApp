using DietApp.Application.Common.Interfaces;
using DietApp.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DietApp.Application.Features.Clients.Commands.DeleteClient;

public class DeleteClientCommandHandler : IRequestHandler<DeleteClientCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public DeleteClientCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == request.ClientId && c.TenantId == request.TenantId, cancellationToken);

        if (client == null)
        {
            throw new NotFoundException("Danışan", request.ClientId);
        }

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
