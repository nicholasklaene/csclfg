using System.Threading.Tasks;
using Xunit;

namespace Tests.Handlers.Command;

public class AuthSigninCommandHandlerTests : HandlerTestsBase
{
    [Fact]
    public Task AuthSigninCommandHandler_ShouldReturnResponseWithNoErrors_WhenUserIsValid()
    {
        // Revisit this, not sure how to mock some of the AWS objects...
        return Task.CompletedTask;
    }
}
