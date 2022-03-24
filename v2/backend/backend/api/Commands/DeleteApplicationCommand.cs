using MediatR;

namespace api.Commands;

public class DeleteApplicationCommand : IRequest<bool>
{
    public short Id { get; }
    
    public DeleteApplicationCommand(short id)
    {
        Id = id;
    }
}

