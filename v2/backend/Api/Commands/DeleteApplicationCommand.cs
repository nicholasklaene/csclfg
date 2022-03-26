using MediatR;

namespace Api.Commands;

public class DeleteApplicationCommand : IRequest<bool>
{
    public short Id { get; }
    
    public DeleteApplicationCommand(short id)
    {
        Id = id;
    }
}

