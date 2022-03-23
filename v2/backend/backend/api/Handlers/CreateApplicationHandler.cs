using api.Commands;
using api.Data;
using api.Models;
using api.Response;
using AutoMapper;
using MediatR;

namespace api.Handlers;

public class CreateApplicationHandler : IRequestHandler<CreateApplicationCommand, CreateApplicationResponse>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateApplicationHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<CreateApplicationResponse> Handle(
        CreateApplicationCommand request, CancellationToken cancellationToken)
    {
        var existingApplication = _db.Applications.FirstOrDefault(
            a => a.Name == request.Name);
        if (existingApplication != null) return null!;
        var application = _mapper.Map<Application>(request);
        await _db.Applications.AddAsync(application, cancellationToken);
        var success = await _db.SaveChangesAsync(cancellationToken) > 0;
        return success ? _mapper.Map<CreateApplicationResponse>(application) : null!;
    }
}
