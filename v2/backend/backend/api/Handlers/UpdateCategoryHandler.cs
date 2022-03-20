using api.Commands;
using api.Data;
using api.Models;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }
    
            
    // TODO: Clean this and create up, there are a few repeated steps
    // Maybe add a repository pattern for write actions
    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
        
        var category = await _db.Categories
                .Include(c => c.CategoryHasSuggestedTags)
                .ThenInclude(ct => ct.Tag)
                .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
        
        if (category == null) return null!;
        
        var tagsToDelete = category.CategoryHasSuggestedTags
            .Where(ct => !request.SuggestedTags
                .Select(st => st.Label)
                .Contains(ct.TagLabel))
            .ToList();
        _db.CategoryHasSuggestedTags.RemoveRange(tagsToDelete);
        var numChanges = await _db.SaveChangesAsync(cancellationToken);
        if (numChanges != tagsToDelete.Count) return null!;
        
        var categoryTagsToAdd = request.SuggestedTags
            .Where(st => !category.CategoryHasSuggestedTags
                .Select(ct => ct.Tag.Label).Contains(st.Label))
            .Select(st => _mapper.Map<Tag>(st))
            .ToList();

        var existingTags = _db.Tags
            .AsNoTracking()
            .Where(t => categoryTagsToAdd.Select(tt => tt.Label).Contains(t.Label))
            .ToList();
        var newTags = categoryTagsToAdd
            .Where(t => !existingTags.Exists(et => et.Label == t.Label))
            .Select(t => _mapper.Map<Tag>(t)).ToList();
        
        if (newTags.Count > 0)
        {
            await _db.Tags.AddRangeAsync(newTags, cancellationToken);
            numChanges = await _db.SaveChangesAsync(cancellationToken);
            if (numChanges != newTags.Count) return null!;
        }

        var newCategoryHasTags = categoryTagsToAdd
            .Select(ct => new CategoryHasSuggestedTag()
                { CategoryId = category.Id, TagLabel = ct.Label })
            .ToList();

        Console.WriteLine("Before doom");
        await _db.CategoryHasSuggestedTags.AddRangeAsync(newCategoryHasTags, cancellationToken);
        numChanges = await _db.SaveChangesAsync(cancellationToken);
        if (numChanges != newCategoryHasTags.Count) return null!;

        if (category.ApplicationId != request.ApplicationId || category.Label != request.Label)
        {
            category.ApplicationId = request.ApplicationId;
            category.Label = request.Label;
            _db.Categories.Update(category);
            numChanges = await _db.SaveChangesAsync(cancellationToken);
            if (numChanges == 0) return null!;
        }

        await transaction.CommitAsync(cancellationToken);
        
        var response = _mapper.Map<UpdateCategoryResponse>(category);
        response.SuggestedTags = request.SuggestedTags.Select(st => st.Label).ToList();

        return response;
    }
}
