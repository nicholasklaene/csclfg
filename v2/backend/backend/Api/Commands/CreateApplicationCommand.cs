using System.ComponentModel.DataAnnotations;
using Api.Response;
using MediatR;

namespace Api.Commands;

public class CreateApplicationCommand : IRequest<CreateApplicationResponse?>
{
    [Required] 
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required] 
    [StringLength(15)] 
    public string Subdomain { get; set; } = null!;
}
