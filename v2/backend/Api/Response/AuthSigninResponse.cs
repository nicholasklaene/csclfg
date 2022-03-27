namespace Api.Response;

public class AuthSigninResponse
{
    public string AccessToken { get; set; } = null!;

    public string IdToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public List<string> Errors { get; set; } = new();
}
