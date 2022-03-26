using System.ComponentModel.DataAnnotations;
using Api.Response;
using Api.ValidationAttributes;
using MediatR;

namespace Api.Commands;

public class UpdateApplicationCommand : IRequest<UpdateApplicationResponse>
{
    [Required]
    [Id]
    public short Id { get; set; }

    [Required(AllowEmptyStrings = false)] 
    [StringLength(50)]
    public string Name { get; set; } = null!;

    [Required(AllowEmptyStrings = false)] 
    [StringLength(15)] 
    public string Subdomain { get; set; } = null!;
}
