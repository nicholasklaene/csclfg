using api.Commands;
using api.Data;
using api.Models;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers.Command;

public class CreateApplicationCommandHandler : IRequestHandler<CreateApplicationCommand, CreateApplicationResponse?>
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;

    public CreateApplicationCommandHandler(ApplicationDbContext db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }
    
    public async Task<CreateApplicationResponse?> Handle(CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        var existingApplication = await _db.Applications.AsNoTracking()
            .FirstOrDefaultAsync(a => a.Name == request.Name || a.Subdomain == request.Subdomain, cancellationToken);

        if (existingApplication != null)
        {
            return null;
        }
        
        var application = _mapper.Map<Application>(request);

        await _db.Applications.AddAsync(application, cancellationToken);

        var numChanges = await _db.SaveChangesAsync(cancellationToken);

        return numChanges > 0 ? _mapper.Map<CreateApplicationResponse>(application) : null;
    }
}
