using FluentAssertions;
using HttpContextMoq.Extensions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace HttpContextMoq.Samples
{
    public class RequestUrlSamples
    {
        private const string scheme = "https";
        private const string host = "localhost";
        private const string path = "mocks";
        private const string query = "?assert=true";
        private readonly string url = $"{scheme}://{host}/{path}{query}";

        [Fact]
        public void MockEntireRequestUrl()
        {
            var context = new HttpContextMock().SetupUrl(url);

            context.Request.Host.Host.Should().Be(host);
            context.Request.QueryString.ToString().Should().Be(query);
        }

        [Fact]
        public void MockRequestUrlProperties()
        {
            var context = new HttpContextMock();

            context.RequestMock.Mock.Setup(r => r.Scheme).Returns(scheme);
            context.RequestMock.Mock.Setup(r => r.Host).Returns(new HostString(host));

            context.Request.Scheme.Should().Be(scheme);
            context.Request.Host.ToString().Should().Be(host);
        }
    }
}
