namespace api.Response;

public class GetCategoryResponse
{
    public int Id { get; set; }
    
    public string Label { get; set; }
    
    public List<string> SuggestedTags { get; set; }
}
