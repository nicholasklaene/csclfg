using MediatR;

namespace api.Commands;

public class DeleteApplicationCommand : IRequest<bool>
{
    public short ApplicationId { get; }

    public DeleteApplicationCommand(short applicationId)
    {
        ApplicationId = applicationId;
    }
}
