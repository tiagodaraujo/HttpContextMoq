using FluentAssertions;
using Xunit;

namespace HttpContextMoq.Samples
{
    public class RequestRouteValuesSamples
    {
        private const string key1 = "key1", value1 = "value1";

        [Fact]
        public void MocRequestRouteValues()
        {
            // Act
            var context = new HttpContextMock();
            context.Request.RouteValues.Add(key1, value1);

            // Assert
            context.Request.RouteValues.ContainsKey(key1).Should().BeTrue();
            context.Request.RouteValues[key1].Should().BeEquivalentTo(value1);
        }
    }
}
