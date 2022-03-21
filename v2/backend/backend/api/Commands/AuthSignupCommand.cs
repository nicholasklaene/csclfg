using System.ComponentModel.DataAnnotations;
using api.Response;
using MediatR;

namespace api.Commands;

public class AuthSignupCommand : IRequest<AuthSignupResponse>
{
    [Required(ErrorMessage = "email is required")]
    public string Email { get; set; }
    
    [Required(ErrorMessage = "username is required")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "password is required")]
    public string Password { get; set; }
}
