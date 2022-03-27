namespace Api.Response;

public class AuthRefreshResponse
{
    public string AccessToken { get; set; } = null!;

    public string IdToken { get; set; } = null!;

    public List<string> Errors { get; } = new();
}
