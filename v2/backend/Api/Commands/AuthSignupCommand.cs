using System.ComponentModel.DataAnnotations;
using Api.Response;
using MediatR;

namespace Api.Commands;

public class AuthSignupCommand : IRequest<AuthSignupResponse>
{
    [Required] 
    [StringLength(50)]
    public string Username { get; set; } = null!;

    [Required] 
    [StringLength(50)]
    public string Email { get; set; } = null!;
    
    [Required] 
    [StringLength(50, MinimumLength = 8)]
    public string Password { get; set; } = null!;
}