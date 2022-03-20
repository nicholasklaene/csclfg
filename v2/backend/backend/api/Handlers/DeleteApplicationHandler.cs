using api.Commands;
using api.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class DeleteApplicationHandler : IRequestHandler<DeleteApplicationCommand, bool>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public DeleteApplicationHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<bool> Handle(DeleteApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await _db.Applications
            .Where(a => a.Id == request.ApplicationId)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (application == null) return false;

        _db.Applications.Remove(application);

        var numChanges = await _db.SaveChangesAsync(cancellationToken);

        return numChanges > 0;
    }
}
