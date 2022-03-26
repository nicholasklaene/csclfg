using Api.Response;
using MediatR;

namespace Api.Queries;

public class GetApplicationByIdQuery : IRequest<GetApplicationByIdResponse?>
{
    public short Id { get; init; }

    public GetApplicationByIdQuery(short id)
    {
        Id = id;
    }
}
