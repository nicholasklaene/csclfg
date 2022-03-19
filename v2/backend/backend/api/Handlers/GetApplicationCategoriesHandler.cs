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
        var categories = _db.Categories.AsNoTracking()
            .Where(c => c.ApplicationId == request.ApplicationId);

        var getCategoriesResponseList =
            categories.Select(c => _mapper.Map<GetCategoryResponse>(c))
                .ToList();

        var getCategoryResponse = new GetApplicationCategoriesResponse(getCategoriesResponseList);

        return Task.FromResult(getCategoryResponse);
    }
}