using System.ComponentModel.DataAnnotations;

namespace api.Request;

public class CreateTagRequest
{
    [Required(ErrorMessage = "label is required")]
    [StringLength(50, ErrorMessage = "label must have length <= 50")]
    public string Label { get; set; }
}
