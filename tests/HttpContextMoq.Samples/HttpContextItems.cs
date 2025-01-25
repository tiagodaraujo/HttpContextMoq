using System.Linq;
using FluentAssertions;
using HttpContextMoq.Extensions;
using Xunit;

namespace HttpContextMoq.Samples;

public class HttpContextItems
{
    [Fact]
    public void Items_WithItemsFakeAndMiddleware_ExpectItemsWithNewItem()
    {
        // Arrange
        var httpContext = new HttpContextMock();
        httpContext.SetupUrl("https://httpcontext.moq/contoso/user/2");
        httpContext.Items = new ItemsDictionaryFake();

        // Act
        MiddlewareInvoke(httpContext);

        // Assert
        httpContext.Items
            .Should().ContainKey("Tenant")
            .WhoseValue.Should().Be("contoso");
    }

    private void MiddlewareInvoke(HttpContextMock httpContext)
    {
        var tenantSegment = httpContext.Request.Path.Value.Split('/').Skip(1).First();
        httpContext.Items.Add("Tenant", tenantSegment);
    }
}
