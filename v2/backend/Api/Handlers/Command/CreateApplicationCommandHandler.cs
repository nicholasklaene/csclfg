using Api.Commands;
using Api.Data;
using Api.Models;
using Api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Handlers.Command;

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
        await _db.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateApplicationResponse>(application);
    }
}
