namespace api.Response;

public class GetApplicationByIdResponse
{
    public short Id { get; set; }

    public string Name { get; set; }

    public string Subdomain { get; set; }

    public List<GetApplicationByIdResponseCategory> Categories { get; set; }
}

public class GetApplicationByIdResponseCategory
{
    public int Id { get; set; }
    
    public string Label { get; set; }
    
    public List<string> Tags { get; set; }
}
