using System.Collections.Generic;
using FluentAssertions;
using HttpContextMoq.Extensions;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace HttpContextMoq.Samples
{
    public class RequestHeadersSamples
    {
        private const string header1 = "header1", value1 = "value1";

        [Fact]
        public void MockEntireRequestHeaders()
        {
            // Act
            var context = new HttpContextMock().SetupRequestHeaders(new Dictionary<string, StringValues> {
                { header1, value1 }
            });

            // Assert
            context.Request.Headers.ContainsKey(header1).Should().BeTrue();
            context.Request.Headers[header1].Should().BeEquivalentTo(value1);
        }
    }
}
