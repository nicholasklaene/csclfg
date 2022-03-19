namespace api.Models;

public class CategoryHasSuggestedTag
{
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    
    public string TagLabel { get; set; }
    public Tag Tag { get; set; }
}
