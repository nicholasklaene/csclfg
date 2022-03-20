namespace api.Response;

public class GetApplicationResponse
{
    public short Id { get; set; }

    public string Name { get; set; }

    public string Subdomain { get; set; }

    public List<GetCategoryResponse> Categories { get; set; }
}
