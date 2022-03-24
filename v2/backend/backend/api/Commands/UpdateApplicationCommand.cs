using System.ComponentModel.DataAnnotations;
using api.Response;
using MediatR;

namespace api.Commands;

public class UpdateApplicationCommand : IRequest<UpdateApplicationResponse?>
{
    [Required]
    public short Id { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    
    [Required]
    [StringLength(15)]
    public string Subdomain { get; set; }
}