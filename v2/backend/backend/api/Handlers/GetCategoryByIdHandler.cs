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
        var category = _db.Categories.AsNoTracking()
            .FirstOrDefault(c => c.Id == request.CategoryId);
        
        var getCategoryResponse = category == null ? null : _mapper.Map<GetCategoryResponse>(category);

        return Task.FromResult(getCategoryResponse);
    }
}