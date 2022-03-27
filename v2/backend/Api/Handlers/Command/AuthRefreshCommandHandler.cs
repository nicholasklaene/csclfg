using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Api.Commands;
using Api.Response;
using MediatR;

namespace Api.Handlers.Command;

public class AuthRefreshCommandHandler : IRequestHandler<AuthRefreshCommand, AuthRefreshResponse>
{
    private readonly IConfiguration _configuration;
    private readonly IAmazonCognitoIdentityProvider _identityClient;

    public AuthRefreshCommandHandler(
        IConfiguration configuration,
        IAmazonCognitoIdentityProvider identityClient
    )
    {
        _configuration = configuration;
        _identityClient = identityClient;
    }
    
    public async Task<AuthRefreshResponse> Handle(AuthRefreshCommand request, CancellationToken cancellationToken)
    {
        var refreshRequest = new InitiateAuthRequest
        {
            ClientId = _configuration["AWSCognito:AppClientId"],
            AuthFlow = AuthFlowType.REFRESH_TOKEN_AUTH,
        };
        refreshRequest.AuthParameters.Add("REFRESH_TOKEN", request.RefreshToken);
        var authResponse = await _identityClient.InitiateAuthAsync(refreshRequest, cancellationToken);

        var response = new AuthRefreshResponse();
        if (authResponse.AuthenticationResult.AccessToken is not null)
        {
            response.AccessToken = authResponse.AuthenticationResult.AccessToken;
            response.IdToken = authResponse.AuthenticationResult.IdToken;
        }
        else
        {
            response.Errors.Add("Error refreshing");   
        }
        
        return response;
    }
}
