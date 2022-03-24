using api.Commands;
using api.Data;
using api.Models;
using api.Repositories;
using api.Response;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand, CreateCategoryResponse>
{
    private readonly TagRepository _tagRepository;
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public CreateCategoryHandler(ApplicationDbContext db, IMapper mapper, TagRepository tagRepository)
    {
        _db = db;
        _mapper = mapper;
        _tagRepository = tagRepository;
    }
    
    public async Task<CreateCategoryResponse> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await _db.Database.BeginTransactionAsync(cancellationToken);

        var application = await GetApplication(request, cancellationToken);
        if (application == null) return null!;

        var tags = await CreateTags(request, cancellationToken);
        var category = await CreateCategory(request, cancellationToken);
        await CreateCategoryHasSuggestedTags(category, tags, cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        var createCategoryResponse = _mapper.Map<CreateCategoryResponse>(category);
        createCategoryResponse.SuggestedTags = tags.Select(t => t.Label).ToList();
        return createCategoryResponse;
    }

    private async Task<Application?> GetApplication(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        return await _db.Applications
            .FirstOrDefaultAsync(a => a.Id == request.ApplicationId, cancellationToken);
    }

    private async Task<List<Tag>> CreateTags(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var requestTags = request.SuggestedTags.Select(t => _mapper.Map<Tag>(t)).ToList();
        var tags = await _tagRepository.AddRangeIfNotExists(requestTags, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return tags;
    }

    private async Task<Category> CreateCategory(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = _mapper.Map<Category>(request);
        await _db.Categories.AddAsync(category, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
        return category;
    }

    private async Task CreateCategoryHasSuggestedTags(Category category, IEnumerable<Tag> tags,
        CancellationToken cancellationToken)
    {
        var categorySuggestedTags = tags
            .Select(t => new CategoryHasSuggestedTag() { TagLabel = t.Label, CategoryId = category.Id})
            .ToList();

        await _db.CategoryHasSuggestedTags.AddRangeAsync(categorySuggestedTags, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }
}
