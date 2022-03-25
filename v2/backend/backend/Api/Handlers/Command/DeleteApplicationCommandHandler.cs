using Api.Commands;
using Api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Handlers.Command;

public class DeleteApplicationCommandHandler : IRequestHandler<DeleteApplicationCommand, bool>
{
    private readonly ApplicationDbContext _db;

    public DeleteApplicationCommandHandler(ApplicationDbContext db)
    {
        _db = db;
    }
    
    public async Task<bool> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await _db.Applications.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (application == null) return false;
        _db.Applications.Remove(application);
        return await _db.SaveChangesAsync(cancellationToken) > 0;
    }
}
