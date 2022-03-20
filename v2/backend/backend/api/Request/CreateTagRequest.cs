using System.ComponentModel.DataAnnotations;

namespace api.Request;

public class CreateTagRequest
{
    [Required]
    [StringLength(50)]
    public string Label { get; set; }
}