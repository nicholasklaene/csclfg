namespace api.Response;

public class GetAllApplicationsResponse
{
    public List<GetApplicationResponse> Applications { get; }

    public GetAllApplicationsResponse(List<GetApplicationResponse> applications)
    {
        Applications = applications;
    } 
}
