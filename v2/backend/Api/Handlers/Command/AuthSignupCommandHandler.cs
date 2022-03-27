using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Api.Commands;
using Api.Data;
using Api.Models;
using Api.Response;
using MediatR;

namespace Api.Handlers.Command;

public class AuthSignupCommandHandler : IRequestHandler<AuthSignupCommand, AuthSignupResponse>
{
    private readonly IConfiguration _configuration;
    private readonly ApplicationDbContext _db;
    private readonly IAmazonCognitoIdentityProvider _identityClient;

    public AuthSignupCommandHandler(IConfiguration configuration, ApplicationDbContext db,
        IAmazonCognitoIdentityProvider identityClient)
    {
        _configuration = configuration;
        _db = db;
        _identityClient = identityClient;
    }

    public async Task<AuthSignupResponse> Handle(AuthSignupCommand request, CancellationToken cancellationToken)
    {
        var signupRequest = new SignUpRequest()
        {
            ClientId = _configuration["AWSCognito:AppClientId"],
            Username = request.Username,
            Password = request.Password 
        };
        var emailAttribute = new AttributeType() { Name = "email", Value = request.Email };
        signupRequest.UserAttributes.Add(emailAttribute);

        var response = new AuthSignupResponse();
        try
        {
            await _identityClient.SignUpAsync(signupRequest, cancellationToken);
            _db.Users.Add(new User() { Username = request.Username, Email = request.Email });
            var numChanges = await _db.SaveChangesAsync(cancellationToken);

            if (numChanges == 0)
            {
                response.Errors.Add("Error creating user");
            }
        }
        catch (UsernameExistsException)
        {
            response.Errors.Add("Username taken");
        }

        return response;
    }
}
