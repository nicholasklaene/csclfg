using api.Data;
using api.Queries;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class GetCategoryByIdHandler : 
    IRequestHandler<GetCategoryByIdQuery, GetCategoryResponse>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public GetCategoryByIdHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public Task<GetCategoryResponse> Handle(
        GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var queryResult = _db.Categories.AsNoTracking()
            .Where(c => c.Id == request.CategoryId)
            .Include(c => c.CategoryHasSuggestedTags)
            .ThenInclude(ct => ct.Tag)
            .ToList()
            .Select(c =>
            {
                var result = _mapper.Map<GetCategoryResponse>(c);
                result.SuggestedTags = c.CategoryHasSuggestedTags.Select(ct => ct.Tag.Label).ToList();
                return result;
            })
            .FirstOrDefault()!;
        return Task.FromResult(queryResult);
    }
}