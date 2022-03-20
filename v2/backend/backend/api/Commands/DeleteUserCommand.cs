using MediatR;

namespace api.Commands;

public class DeleteUserCommand : IRequest<bool>
{
    public string Username { get; set; }

    public DeleteUserCommand(string username)
    {
        Username = username;
    }
}
