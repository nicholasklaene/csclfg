namespace api.Models;

public class Category
{
    public int Id { get; set; }
    public short ApplicationId { get; set; }
    public Application Application { get; set; }
    public string Label { get; set; }
 
    public virtual ICollection<CategoryHasSuggestedTag> CategoryHasSuggestedTags { get; set; }
    
    public ICollection<Post> Posts { get; set; }
}
