namespace Api.Response;

public class GetAllApplicationsResponse
{
    public List<GetAllApplicationsResponseApplication> Applications { get; set; }
}

public class GetAllApplicationsResponseApplication
{
    public short Id { get; set; }

    public string Name { get; set; }

    public string Subdomain { get; set; }

    public List<GetAllApplicationsResponseApplicationCategory> Categories { get; set; }
}

public class GetAllApplicationsResponseApplicationCategory
{
    public int Id { get; set; }
    
    public string Label { get; set; }
    
    public List<string> Tags { get; set; }
}
