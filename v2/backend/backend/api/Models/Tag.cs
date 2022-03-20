namespace api.Models;

public class Tag
{
    public string Label { get; set; }
    
    public ICollection<CategoryHasSuggestedTag> CategoryHasSuggestedTags { get; set; }

    public ICollection<PostHasTag> PostHasTags { get; set; }
}
