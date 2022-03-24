using api.Commands;
using api.Data;
using api.Models;
using api.Repositories;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
{
    private readonly TagRepository _tagRepository; 
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public UpdateCategoryHandler(ApplicationDbContext db, IMapper mapper, TagRepository tagRepository)
    {
        _db = db;
        _mapper = mapper;
        _tagRepository = tagRepository;
    }

    public async Task<UpdateCategoryResponse> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);
        
        var category = await GetCategory(request, cancellationToken);
        if (category == null) return null!;
        
        var application = await GetApplication(request, cancellationToken);
        if (application == null) return null!;
        
        await UpdateCategory(category, request, cancellationToken);
        await UpdateTags(category, request, cancellationToken);
        await UpdateCategoryHasSuggestedTags(category, request, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        
        var response = _mapper.Map<UpdateCategoryResponse>(category);
        response.SuggestedTags = request.SuggestedTags.Select(st => st.Label).ToList();
        return response;
    }
    
    private async Task<Application?> GetApplication(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await _db.Applications
            .FirstOrDefaultAsync(a => a.Id == request.ApplicationId, cancellationToken);
    }

    private async Task<Category?> GetCategory(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await _db.Categories
            .Include(c => c.CategoryHasSuggestedTags)
            .ThenInclude(ct => ct.Tag)
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);
    }
    
    private async Task UpdateCategory(Category category, UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        category.ApplicationId = request.ApplicationId;
        category.Label = request.Label;
        _db.Categories.Update(category);
        await _db.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateTags(Category category, UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var tagsToDelete = category.CategoryHasSuggestedTags
            .Where(ct => !request.SuggestedTags.Select(st => st.Label).Contains(ct.TagLabel))
            .ToList();
        _db.CategoryHasSuggestedTags.RemoveRange(tagsToDelete);

        var requestTags = request.SuggestedTags.Select(t => _mapper.Map<Tag>(t)).ToList();
        await _tagRepository.AddRangeIfNotExists(requestTags, cancellationToken);
        
        await _db.SaveChangesAsync(cancellationToken);
    }

    private async Task UpdateCategoryHasSuggestedTags(Category category, UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        var newCategoryHasTags = request.SuggestedTags
            .Where(st => !category.CategoryHasSuggestedTags
                .Select(ct => ct.Tag.Label).Contains(st.Label))
            .Select(ct => new CategoryHasSuggestedTag() {CategoryId = category.Id, TagLabel = ct.Label})
            .ToList();
        await _db.CategoryHasSuggestedTags.AddRangeAsync(newCategoryHasTags, cancellationToken);
        
        await _db.SaveChangesAsync(cancellationToken);
    }
}
