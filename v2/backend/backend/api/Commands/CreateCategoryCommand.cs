using System.ComponentModel.DataAnnotations;
using api.ValidationAttributes;
using api.Request;
using api.Response;
using MediatR;

namespace api.Commands;

public class CreateCategoryCommand : IRequest<CreateCategoryResponse>
{
    [Required(ErrorMessage = "applicationId is required")] 
    public short ApplicationId { get; set; }
    
    [Required(ErrorMessage = "label is required")]
    [StringLength(50, ErrorMessage = "label length must be <= 50")]
    public string Label { get; set; }
    
    [Required(ErrorMessage = "suggestedTags is required")]
    [MaxLengthOf(5, ErrorMessage = "suggestedTags length must be <= 5")]
    public List<CreateTagRequest> SuggestedTags { get; set; }
}
