using api.Data;
using api.Queries;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var queryResult = _db.Applications.AsNoTracking()
            .Include(a => a.Categories)
            .ThenInclude(c => c.CategoryHasSuggestedTags)
            .ThenInclude(ct => ct.Tag)
            .ToList()
            .Select(a =>
            {
                var result = _mapper.Map<GetApplicationResponse>(a);
                result.Categories = a.Categories.ToList().Select(c =>
                {
                    var getCategoryResponse = _mapper.Map<GetCategoryResponse>(c);
                    getCategoryResponse.SuggestedTags = c.CategoryHasSuggestedTags.Select(ct => ct.Tag.Label).ToList();
                    return getCategoryResponse;
                }).ToList();
                return result;
            })
            .ToList();
        var response = new GetAllApplicationsResponse(queryResult);
        return Task.FromResult(response);
    }
}
