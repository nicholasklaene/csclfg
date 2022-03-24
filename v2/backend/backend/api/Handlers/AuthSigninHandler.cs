using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Extensions.CognitoAuthentication;
using api.Queries;
using api.Response;
using MediatR;

namespace api.Handlers;

public class AuthSigninHandler : IRequestHandler<AuthSigninQuery, AuthSigninResponse>
{
    private readonly IConfiguration _configuration;
    private readonly AmazonCognitoIdentityProviderClient _identityClient;
    private readonly CognitoUserPool _userPool;
    
    public AuthSigninHandler(
        IConfiguration configuration,
        AmazonCognitoIdentityProviderClient identityClient,
        CognitoUserPool userPool
    )
    {
        _configuration = configuration;
        _identityClient = identityClient;
        _userPool = userPool;
    }
    
    public async Task<AuthSigninResponse> Handle(AuthSigninQuery request, CancellationToken cancellationToken)
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
            
            var cookieOptions = new CookieOptions() 
                { 
                    Secure = true,
                    HttpOnly = true,
                    SameSite = SameSiteMode.None, 
                    Expires = DateTimeOffset.Now.AddDays(1) 
                };
            request.Response!.Cookies.Append("refresh_token", refreshToken, cookieOptions);
        }
        catch (NotAuthorizedException)
        {
            response.Errors.Add("Invalid password");
        }
        catch (UserNotFoundException)
        {
            response.Errors.Add("User does not exist");
        }
        catch (UserNotConfirmedException)
        {
            response.Errors.Add("You must confirm your email to continue");
        }
        
        return response;
    }
}
