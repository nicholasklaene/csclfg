using System.ComponentModel.DataAnnotations;
using api.Response;
using MediatR;

namespace api.Commands;

public class CreateCategoryCommand : IRequest<CreateCategoryResponse>
{
    [Required] 
    public short ApplicationId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Label { get; set; }
}
