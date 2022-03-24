using System.Linq;
using System.Threading.Tasks;
using api.Handlers.Query;
using api.Queries;
using Xunit;

namespace tests.Handlers.Query;

[Collection("GetAllApplicationsQueryHandlerTests")]
public class GetAllApplicationsQueryHandlerTests : HandlerTestsBase
{
    [Fact]
    public async Task GetAllApplicationsQueryHandler_ShouldReturnAllApplicationsWithCategoriesAndTags_Always()
    {
        // Arrange
        var query = new GetAllApplicationsQuery();
        var handler = new GetAllApplicationsQueryHandler(Db, Mapper);
        // Act
        var result = await handler.Handle(query, CancellationToken);
        // Assert
        var application1 = result.Applications.First(a => a.Id == 1);
        var application2 = result.Applications.First(a => a.Id == 2);
        
        Assert.NotNull(application1);
        Assert.NotNull(application2);
        
        Assert.Equal("Computer Science", application1.Name);
        Assert.Equal("cs", application1.Subdomain);
        Assert.Equal("taken", application2.Name);
        Assert.Equal("taken", application2.Subdomain);
        
        var application1Category1 = application1.Categories.First(c => c.Label == "Algorithms");
        var application1Category2 = application1.Categories.First(c => c.Label == "Operating Systems");
        Assert.NotNull(application1Category1);
        Assert.NotNull(application1Category2);
        Assert.Contains(application1Category1.Tags, t => t == "discord");
        Assert.Contains(application1Category1.Tags, t => t == "python");
        Assert.Contains(application1Category2.Tags, t => t == "discord");
        Assert.Contains(application1Category2.Tags, t => t == "daily");
    }
}