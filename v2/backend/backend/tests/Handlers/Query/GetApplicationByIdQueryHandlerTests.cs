using System.Linq;
using System.Threading.Tasks;
using api.Handlers.Query;
using api.Queries;
using Xunit;

namespace tests.Handlers.Query;

[Collection("GetApplicationByIdQueryHandlerTests")]
public class GetApplicationByIdQueryHandlerTests : HandlerTestsBase
{
    [Fact]
    public async Task GetApplicationByIdQueryHandler_ShouldReturnApplication_WhenApplicationExists()
    {
        // Arrange
        var query = new GetApplicationByIdQuery() { Id = 1 };
        var handler = new GetApplicationByIdQueryHandler(Db, Mapper);
        // Act
        var response = await handler.Handle(query, CancellationToken);
        // Assert
        Assert.NotNull(response);
        Assert.Equal("Computer Science", response!.Name);
        Assert.Equal("cs", response.Subdomain);
    }

    [Fact]
    public async Task GetApplicationByIdQueryHandler_ShouldReturnApplicationWithCorrectCategories_WhenApplicationExists()
    {
        // Arrange
        var query = new GetApplicationByIdQuery() { Id = 1 };
        var handler = new GetApplicationByIdQueryHandler(Db, Mapper);
        // Act
        var response = await handler.Handle(query, CancellationToken);
        // Assert
        Assert.NotEmpty(response!.Categories);
        Assert.Equal(2, response.Categories.Count);
        Assert.Contains(response.Categories, c => c.Id == 1);
        Assert.Contains(response.Categories, c => c.Id == 2);
    }

    [Fact]
    public async Task
        GetApplicationByIdQueryHandler_ShouldReturnApplicationWithCategoriesAndCorrectTags_WhenApplicationExists()
    {
        // Arrange
        var query = new GetApplicationByIdQuery() { Id = 1 };
        var handler = new GetApplicationByIdQueryHandler(Db, Mapper);
        // Act
        var response = await handler.Handle(query, CancellationToken);
        var category1 = response!.Categories.First(c => c.Id == 1);
        var category2 = response.Categories.First(c => c.Id == 2);
        // Assert
        Assert.NotEmpty(category1.Tags);
        Assert.Equal(2, category1.Tags.Count);
        Assert.Contains(category1.Tags, t => t == "discord");
        Assert.Contains(category1.Tags, t => t == "python");
        
        Assert.NotEmpty(category2.Tags);
        Assert.Equal(2, category2.Tags.Count);
        Assert.Contains(category2.Tags, t => t == "discord");
        Assert.Contains(category2.Tags, t => t == "daily");
    }
    
    [Fact]
    public async Task GetApplicationByIdQueryHandler_ShouldNotReturnApplication_WhenApplicationDoesNotExist()
    {
        // Arrange
        var query = new GetApplicationByIdQuery() { Id = 500 };
        var handler = new GetApplicationByIdQueryHandler(Db, Mapper);
        // Act
        var response = await handler.Handle(query, CancellationToken);
        // Assert
        Assert.Null(response);
    }
}
