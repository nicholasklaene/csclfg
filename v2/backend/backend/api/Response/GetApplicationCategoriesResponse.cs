namespace api.Response;
public class GetApplicationCategoriesResponse
{
    public List<GetCategoryResponse> Categories { get; set; }

    public GetApplicationCategoriesResponse(List<GetCategoryResponse> categories)
    {
        Categories = categories;
    }
}
