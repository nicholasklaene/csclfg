using api.Data;
using api.Queries;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class GetApplicationCategoriesHandler : 
    IRequestHandler<GetApplicationCategoriesQuery, GetApplicationCategoriesResponse>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public GetApplicationCategoriesHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public Task<GetApplicationCategoriesResponse> Handle(
        GetApplicationCategoriesQuery request, CancellationToken cancellationToken)
    {
        var queryResult = _db.Categories.AsNoTracking()
            .Where(c => c.ApplicationId == request.ApplicationId)
            .Include(c => c.CategoryHasSuggestedTags)
            .ThenInclude(ct => ct.Tag)
            .ToList()
            .Select(c => {
                var result = _mapper.Map<GetCategoryResponse>(c);
                result.SuggestedTags = c.CategoryHasSuggestedTags
                    .Select(ct => ct.Tag.Label)
                    .ToList();
                return result;
            }).ToList();
        var response = new GetApplicationCategoriesResponse(queryResult);
        return Task.FromResult(response);
    }
}
