using api.Data;
using api.Queries;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class GetApplicationByIdHandler : IRequestHandler<GetApplicationByIdQuery, GetApplicationResponse>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public GetApplicationByIdHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public Task<GetApplicationResponse> Handle(GetApplicationByIdQuery request, CancellationToken cancellationToken)
    {
        var queryResult = _db.Applications.AsNoTracking()
            .Where(a => a.Id == request.Id)
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
            .FirstOrDefault()!;
        return Task.FromResult(queryResult);
    }
}