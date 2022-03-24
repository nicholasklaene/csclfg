using api.Commands;
using api.Data;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers.Command;

public class UpdateApplicationCommandHandler : IRequestHandler<UpdateApplicationCommand, UpdateApplicationResponse?>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateApplicationCommandHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<UpdateApplicationResponse?> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        var applications = _db.Applications
            .Where(a => a.Id == request.Id || a.Name == request.Name || a.Subdomain == request.Subdomain)
            .ToList();

        if (applications.Count is 0 or > 1) return null;

        var application = applications.First();
        
        application.Name = request.Name;
        application.Subdomain = request.Subdomain;
        _db.Applications.Update(application);
        await _db.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UpdateApplicationResponse>(application);
    }
}
