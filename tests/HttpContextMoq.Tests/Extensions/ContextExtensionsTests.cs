using System;
using System.Linq;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Xunit;

namespace HttpContextMoq.Extensions.Tests
{
    public class ContextExtensionsTests
    {
        [Theory]
        [InlineData(null, typeof(ArgumentNullException))]
        [InlineData("", typeof(UriFormatException))]
        [InlineData("invalid", typeof(UriFormatException))]
        [InlineData("http:/localhost", typeof(UriFormatException))]
        public void SetupUrl_WhenUrlIsInvalid_ThrowsException(string url, Type exception)
        {
            // Arrange
            var context = new HttpContextMockBuilder().Build();

            // Act
            void act() => context.SetupUrl(url);

            // Assert
            Assert.Throws(exception, act);
        }


        [Theory]
        [InlineData("https://localhost", "param", "", 0, false)]
        [InlineData("https://localhost?param=value", "param", "value", 1, true)]
        public void SetupUrl_WhenUrlIsValid_QueryShouldBeMocked(string url, string queryParam, string queryValue, int queryCount, bool queryExist)
        {
            // Arrange
            var context = new HttpContextMockBuilder().Build();

            // Act
            context.SetupUrl(url);
            var query = context.Request.Query;

            // Assert
            query.ContainsKey(queryParam).Should().Be(queryExist);
            query.Count.Should().Be(queryCount);
            query.Keys.Should().HaveCount(queryCount);
            query.Keys.Where(k => k == queryParam).Should().HaveCount(queryCount);
            query.TryGetValue(queryParam, out var tryGetValue).Should().Be(queryExist);
            tryGetValue.ToString().Should().Be(queryValue);
            query[queryParam].ToString().Should().Be(queryValue);
            query.Where(i => i.Key == queryParam).Should().HaveCount(queryCount);
        }

        [Theory]
        [InlineData("https://localhost", true, "https", "localhost", "/", "")]
        [InlineData("http://localhost:80", false, "http", "localhost", "/", "")]
        [InlineData("https://localhost:443", true, "https", "localhost", "/", "")]
        [InlineData("https://localhost/path", true, "https", "localhost", "/path", "")]
        [InlineData("https://localhost:123/path?query=asd", true, "https", "localhost:123", "/path", "?query=asd")]
        [InlineData("https://localhost/?query", true, "https", "localhost", "/", "?query")]
        public void SetupUrl_WhenUrlIsValid_RequestShouldBeMocked(string url, bool isHttps, string scheme, string host, string path, string queryString)
        {
            // Arrange
            var context = new HttpContextMockBuilder().Build();
            var queryDictionary = QueryHelpers.ParseQuery(queryString);

            // Act
            context.SetupUrl(url);
            var request = context.Request;
            var requestFeature = context.Features.Get<IHttpRequestFeature>();

            // Assert
            request.IsHttps.Should().Be(isHttps);
            request.Scheme.Should().Be(scheme);
            request.Host.ToString().Should().Be(host);
            request.PathBase.ToString().Should().Be(string.Empty);
            request.Path.ToString().Should().Be(path);
            request.QueryString.ToString().Should().Be(queryString);
            request.Query.Should().BeEquivalentTo(queryDictionary);
            requestFeature.RawTarget.Should().Be(path + queryString);
        }
    }
}
