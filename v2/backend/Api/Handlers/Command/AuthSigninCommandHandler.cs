using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using Api.Commands;
using Api.Response;
using MediatR;
namespace Api.Handlers.Command;

public class AuthSigninCommandHandler : IRequestHandler<AuthSigninCommand, AuthSigninResponse>
{
    private readonly IConfiguration _configuration;
    private readonly IAmazonCognitoIdentityProvider _identityClient;
    private readonly CognitoUserPool _userPool;

    public AuthSigninCommandHandler(
        IConfiguration configuration,
        IAmazonCognitoIdentityProvider identityClient,
        CognitoUserPool userPool
    )
    {
        _configuration = configuration;
        _identityClient = identityClient;
        _userPool = userPool;
    }
    
    public async Task<AuthSigninResponse> Handle(AuthSigninCommand request, CancellationToken cancellationToken)
    {
        var clientId = _configuration.GetValue<string>("AWSCognito:AppClientId");
        var cognitoUser = new CognitoUser(request.Username, clientId, _userPool, _identityClient);
        var authRequest = new InitiateSrpAuthRequest() { Password = request.Password };

        var response = new AuthSigninResponse();
        try
        {
            var authResponse = await cognitoUser
                .StartWithSrpAuthAsync(authRequest)
                .ConfigureAwait(false);

            var accessToken = authResponse.AuthenticationResult.AccessToken;
            var idToken = authResponse.AuthenticationResult.IdToken;
            var refreshToken = authResponse.AuthenticationResult.RefreshToken;

            response.AccessToken = accessToken;
            response.IdToken = idToken;
            response.RefreshToken = refreshToken;
        }
        catch (NotAuthorizedException)
        {
            response.Errors.Add("Invalid password");
        }
        catch (UserNotFoundException)
        {
            response.Errors.Add("User does not exist");
        }
        
        return response;
    }
}
