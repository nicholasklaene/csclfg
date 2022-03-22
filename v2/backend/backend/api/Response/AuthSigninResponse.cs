namespace api.Response;

public class AuthSigninResponse
{
    public string? AccessToken { get; set; }

    public string? IdToken { get; set; }
    
    public List<string> Errors { get; }

    public AuthSigninResponse()
    {
        Errors = new List<string>();
    }
}
