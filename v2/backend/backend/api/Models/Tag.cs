namespace api.Models;

public class Tag
{
    public string Label { get; set; }
    
    public virtual ICollection<CategoryHasSuggestedTag> CategoryHasSuggestedTags { get; set; }
    
    public virtual ICollection<PostHasTag> PostHasTags { get; set; }
}
