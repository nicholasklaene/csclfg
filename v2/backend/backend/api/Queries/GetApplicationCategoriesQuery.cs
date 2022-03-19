using api.Response;
using MediatR;

namespace api.Queries;

public class GetApplicationCategoriesQuery : IRequest<GetApplicationCategoriesResponse>
{
    public short ApplicationId { get; }
    public GetApplicationCategoriesQuery(short applicationId)
    {
        ApplicationId = applicationId;
    }
}