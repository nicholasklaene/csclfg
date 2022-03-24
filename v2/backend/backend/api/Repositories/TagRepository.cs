using api.Data;
using api.Models;

namespace api.Repositories;

public class TagRepository
{
    private readonly ApplicationDbContext _db;

    public TagRepository(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<List<Tag>> AddRangeIfNotExists(List<Tag> tags, CancellationToken cancellationToken)
    {
        var existingTags = _db.Tags
            .Where(t => tags.Select(tt => tt.Label).Contains(t.Label))
            .ToList();
        
        var newTags = tags
            .Where(t => !existingTags.Exists(et => et.Label == t.Label))
            .ToList();
        
        await _db.Tags.AddRangeAsync(newTags, cancellationToken);
        
        var allTags = newTags
                .Union(existingTags)
                .ToList();

        return allTags;
    }
}
