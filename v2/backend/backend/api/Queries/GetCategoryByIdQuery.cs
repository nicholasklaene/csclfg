using api.Response;
using MediatR;

namespace api.Queries;

public class GetCategoryByIdQuery :  IRequest<GetCategoryResponse>
{
    public int CategoryId { get; }
    public GetCategoryByIdQuery(int categoryId)
    {
        CategoryId = categoryId;
    }
}