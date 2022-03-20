using MediatR;

namespace api.Commands;

public class DeleteCategoryCommand : IRequest<bool>
{
    public int CategoryId { get; }

    public DeleteCategoryCommand(int categoryId)
    {
        CategoryId = categoryId;
    }
}
