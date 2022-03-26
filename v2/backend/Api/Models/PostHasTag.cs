namespace Api.Models;

public class PostHasTag
{
    public long PostId { get; set; }
    public Post Post { get; set; }
    
    public string TagLabel { get; set; }
    public Tag Tag { get; set; }
}
