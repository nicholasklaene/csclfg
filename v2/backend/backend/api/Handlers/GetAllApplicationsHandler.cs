using api.Data;
using api.Queries;
using api.Response;
using AutoMapper;
using MediatR;

namespace api.Handlers;

public class GetAllApplicationsHandler : IRequestHandler<GetAllApplicationsQuery, GetAllApplicationsResponse>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public GetAllApplicationsHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    public Task<GetAllApplicationsResponse> Handle(GetAllApplicationsQuery request, CancellationToken cancellationToken)
    {
        var applications = _db.Applications
            .Select(a => _mapper.Map<GetApplicationResponse>(a))
            .ToList();

        return Task.FromResult(new GetAllApplicationsResponse(applications));
    }
}
