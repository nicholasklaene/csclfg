using System.ComponentModel.DataAnnotations;
using api.Request;
using api.Response;
using api.ValidationAttributes;
using MediatR;

namespace api.Commands;

public class UpdateCategoryCommand : IRequest<UpdateCategoryResponse>
{
    [Required(ErrorMessage = "id is required")]
    public int Id { get; set; }
    
    [Required(ErrorMessage = "applicationId is required")] 
    public short ApplicationId { get; set; }
    
    [Required(ErrorMessage = "label is required")]
    [StringLength(50, ErrorMessage = "label length must be <= 50")]
    public string Label { get; set; }
    
    [Required(ErrorMessage = "suggestedTags is required")]
    [MaxLengthOf(5, ErrorMessage = "suggestedTags length must be <= 5")]
    public List<UpdateTagRequest> SuggestedTags { get; set; }
}
