using api.Response;
using MediatR;

namespace api.Queries;

public class GetApplicationByIdQuery : IRequest<GetApplicationByIdResponse?>
{
    public short Id { get; set; }
}
