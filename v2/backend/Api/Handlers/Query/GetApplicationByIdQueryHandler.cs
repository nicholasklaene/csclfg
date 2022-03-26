using Api.Data;
using Api.Queries;
using Api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Api.Handlers.Query;

public class GetApplicationByIdQueryHandler : IRequestHandler<GetApplicationByIdQuery, GetApplicationByIdResponse?>
{
    private readonly IMapper _mapper;
    private readonly ApplicationDbContext _db;

    public GetApplicationByIdQueryHandler(ApplicationDbContext db, IMapper mapper)
    {
        _mapper = mapper;
        _db = db;
    }
    
    public async Task<GetApplicationByIdResponse?> Handle(GetApplicationByIdQuery request, 
        CancellationToken cancellationToken)
    {
        var application = await _db.Applications
            .Include(a => a.Categories)
            .ThenInclude(c => c.CategoryHasSuggestedTags)
            .FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);

        if (application == null) return null;

        var response = _mapper.Map<GetApplicationByIdResponse>(application);
        response.Categories = application.Categories
            .Select(c => new GetApplicationByIdResponseCategory()
                { Id = c.Id, Label = c.Label, Tags = c.CategoryHasSuggestedTags.Select(ct => ct.TagLabel).ToList() })
            .ToList();
        
        return response;
    }
}
