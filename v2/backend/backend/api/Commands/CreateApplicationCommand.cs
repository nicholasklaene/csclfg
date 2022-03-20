using System.ComponentModel.DataAnnotations;
using api.Response;
using MediatR;

namespace api.Commands;

public class CreateApplicationCommand : IRequest<CreateApplicationResponse>
{
    [Required(ErrorMessage = "name is required")]
    [StringLength(50, ErrorMessage = "name length must be <= 50")]
    public string Name { get; set; }

    [Required(ErrorMessage = "subdomain is required")]
    [StringLength(15, ErrorMessage = "subdomain length must be <= 15")]
    public string Subdomain { get; set; }
}
