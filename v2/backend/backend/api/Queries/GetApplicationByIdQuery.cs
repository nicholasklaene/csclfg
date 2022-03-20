using api.Response;
using MediatR;

namespace api.Queries;

public class GetApplicationByIdQuery : IRequest<GetApplicationResponse>
{
    public readonly short Id;

    public GetApplicationByIdQuery(short Id)
    {
        this.Id = Id;
    }
}
