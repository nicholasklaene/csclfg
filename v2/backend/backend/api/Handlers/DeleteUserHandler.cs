using api.Commands;
using api.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace api.Handlers;

public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
{
    private readonly ApplicationDbContext _db;

    public DeleteUserHandler(ApplicationDbContext db)
    {
        _db = db;
    }
    
    // TODO: Delete from cognito
    public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);
        if (user == null) return false;
        _db.Users.Remove(user);
        var numChanges = await _db.SaveChangesAsync(cancellationToken);
        return numChanges > 0;
    }
}
