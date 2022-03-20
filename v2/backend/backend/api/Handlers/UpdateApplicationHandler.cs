using api.Commands;
using api.Data;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class UpdateApplicationHandler : IRequestHandler<UpdateApplicationCommand, UpdateApplicationResponse>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateApplicationHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<UpdateApplicationResponse> Handle(UpdateApplicationCommand request, CancellationToken cancellationToken)
    {
        var application = await _db.Applications.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (application == null) return null!;

        if (application.Name != request.Name || application.Subdomain != request.Subdomain)
        {
            application.Name = request.Name;
            application.Subdomain = request.Subdomain;
            _db.Applications.Update(application);
            var numChanges = await _db.SaveChangesAsync(cancellationToken);
            if (numChanges == 0) return null!;
        }
        
        var response = _mapper.Map<UpdateApplicationResponse>(application);
        return response;
    }
}
