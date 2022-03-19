namespace api.Models;
public class Application
{
    public short Id { get; set; }
    
    public string Name { get; set; }
    
    public string Subdomain { get; set; }
    
    public ICollection<Category> Categories { get; set; }
}
