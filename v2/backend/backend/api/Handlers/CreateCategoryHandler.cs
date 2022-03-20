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

    // TODO: Abstract tag into helper since it will be needed for update and for post entity actions
    public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
        
        var application = await _db.Applications.FirstOrDefaultAsync(a => a.Id == request.ApplicationId, 
            cancellationToken);
        if (application == null) return null!;

        var existingTags = _db.Tags
            .Where(t => request.SuggestedTags.Select(tt => tt.Label).Contains(t.Label))
            .ToList();
        
        var newTags = request.SuggestedTags.Where(t => !existingTags.Exists(et => et.Label == t.Label))
            .Select(t => _mapper.Map<Tag>(t)).ToList();
        await _db.Tags.AddRangeAsync(newTags, cancellationToken);

        var numChanges = await _db.SaveChangesAsync(cancellationToken);
        if (numChanges != newTags.Count) return null!;

        var category = _mapper.Map<Category>(request);
        await _db.Categories.AddAsync(category, cancellationToken);
        numChanges = await _db.SaveChangesAsync(cancellationToken);
        if (numChanges == 0) return null!;

        var categorySuggestedTags = newTags.Union(existingTags).Select(t => new CategoryHasSuggestedTag()
        {
            Tag = t, TagLabel = t.Label, Category = category, CategoryId = category.Id
        }).ToList();

        await _db.CategoryHasSuggestedTags.AddRangeAsync(categorySuggestedTags, cancellationToken);
        numChanges = await _db.SaveChangesAsync(cancellationToken);
        if (numChanges != categorySuggestedTags.Count) return null!;
        
        await transaction.CommitAsync(cancellationToken);

        var createCategoryResponse = _mapper.Map<CreateCategoryResponse>(category);
        createCategoryResponse.SuggestedTags = categorySuggestedTags.Select(t => t.TagLabel).ToList();

        return createCategoryResponse;
    }
}
