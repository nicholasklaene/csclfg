using Api.Response;
using MediatR;

namespace Api.Commands;

public class AuthRefreshCommand : IRequest<AuthRefreshResponse>
{
    public string RefreshToken { get; set; } = null!;
}
