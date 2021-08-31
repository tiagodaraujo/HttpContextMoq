using System.Collections.Generic;
using FluentAssertions;
using HttpContextMoq.Extensions;
using Xunit;

namespace HttpContextMoq.Samples
{
    public class RequestCookiesSamples
    {
        private const string cookie1 = "cookie1", value1 = "value1";

        [Fact]
        public void MockEntireRequestHeaders()
        {
            // Act
            var context = new HttpContextMock().SetupRequestCookies(new Dictionary<string, string> {
                { cookie1, value1 }
            });

            // Assert
            context.Request.Cookies.ContainsKey(cookie1).Should().BeTrue();
            context.Request.Cookies[cookie1].Should().BeEquivalentTo(value1);
        }
    }
}
