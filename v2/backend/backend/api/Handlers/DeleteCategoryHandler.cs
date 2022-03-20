using api.Commands;
using api.Data;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class DeleteCategoryHandler : IRequestHandler<DeleteCategoryCommand, bool>
{
    private readonly ApplicationDbContext _db;
    private readonly IMapper _mapper;

    public DeleteCategoryHandler(ApplicationDbContext db, IMapper mapper)
    {
        _db = db;
        _mapper = mapper;
    }

    public async Task<bool> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _db.Categories
            .Where(c => c.Id == request.CategoryId)
            .FirstOrDefaultAsync(cancellationToken);

        if (category == null) return false;

        _db.Categories.Remove(category);

        var numChanges = await _db.SaveChangesAsync(cancellationToken);

        return numChanges > 0;
    }
}