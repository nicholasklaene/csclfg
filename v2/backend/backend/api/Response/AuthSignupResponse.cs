namespace api.Response;

public class AuthSignupResponse
{
    public List<string> Errors { get; }
    
    public AuthSignupResponse()
    {
        Errors = new List<string>();
    }
}
