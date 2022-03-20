using System.ComponentModel.DataAnnotations;
using api.Response;
using MediatR;

namespace api.Queries;

public class AuthSigninQuery : IRequest<AuthSigninResponse>
{
    [Required(ErrorMessage = "username is required")]
    public string Username { get; set; }
    
    [Required(ErrorMessage = "password is required")]
    public string Password { get; set; }
}
