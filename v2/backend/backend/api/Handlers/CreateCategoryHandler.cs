using System.Data.SqlTypes;
using api.Commands;
using api.Data;
using api.Models;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;
    
    public CreateCategoryHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
    public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var application = await _db.Applications
            .FirstOrDefaultAsync(a => a.Id == request.ApplicationId);
        
        if (application == null) return null!;
        
        var category = _mapper.Map<Category>(request);
        await _db.AddAsync(category);
        var numChanges = await _db.SaveChangesAsync();
        
        return numChanges > 0 ? _mapper.Map<CreateCategoryResponse>(category) : null!;
    }
}
