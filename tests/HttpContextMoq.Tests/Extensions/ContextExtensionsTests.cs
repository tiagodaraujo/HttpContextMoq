using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using HttpContextMoq.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace HttpContextMoq.Tests.Extensions;

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
        var context = new HttpContextMock();

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
        var context = new HttpContextMock();

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
        var context = new HttpContextMock();
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

    [Fact]
    public void SetupRequestHeaders_WhenEmpty_ShouldNoHeaders()
    {
        // Arrange
        var context = new HttpContextMock();
        var headers = new Dictionary<string, StringValues>();

        // Act
        context.SetupRequestHeaders(headers);

        // Assert
        context.Request.Headers.Count.Should().Be(0);
        context.Request.Headers.Should().HaveCount(0);
    }

    [Fact]
    public void SetupRequestHeaders_WhenHaveHeaders_ShouldHaveHeaders()
    {
        // Arrange
        const string header1 = "header1", header2 = "header2", value1 = "value1", value2 = "value2";
        var context = new HttpContextMock();
        var headers = new Dictionary<string, StringValues> {
            { header1, value1 },
            { header2, value2 }
        };

        // Act
        context.SetupRequestHeaders(headers);

        // Assert
        context.Request.Headers.Count.Should().Be(headers.Count);
        context.Request.Headers.Should().HaveCount(headers.Count);
        context.Request.Headers.Should().BeEquivalentTo(headers);
        foreach (var item in headers)
        {
            context.Request.Headers[item.Key].Should().BeEquivalentTo(item.Value);
            context.Request.Headers.ContainsKey(item.Key).Should().BeTrue();
            context.Request.Headers.Contains(new KeyValuePair<string, StringValues>(item.Key, item.Value)).Should().BeTrue();
            context.Request.Headers.TryGetValue(item.Key, out var value).Should().BeTrue();
            value.Should().BeEquivalentTo(item.Value);
        }

        context.Request.Headers["notexist"].Should().BeEquivalentTo(StringValues.Empty);
        context.Request.Headers.ContainsKey("notexist").Should().BeFalse();
        context.Request.Headers.Contains(new KeyValuePair<string, StringValues>("notexist", "notexist")).Should().BeFalse();
        context.Request.Headers.TryGetValue("notexist", out var valueNotExist).Should().BeFalse();
        valueNotExist.Should().BeEmpty();
    }

    [Fact]
    public void SetupRequestCookies_WhenEmpty_ShouldNoCookies()
    {
        // Arrange
        var context = new HttpContextMock();
        var headers = new Dictionary<string, string>();

        // Act
        context.SetupRequestCookies(headers);

        // Assert
        context.Request.Cookies.Count.Should().Be(0);
        context.Request.Cookies.Should().HaveCount(0);
    }

    [Fact]
    public void SetupRequestCookies_WhenHaveCookies_ShouldHaveCookies()
    {
        // Arrange
        const string cookie1 = "cookie1", cookie2 = "cookie2", value1 = "value1", value2 = "value2";
        var context = new HttpContextMock();
        var cookies = new Dictionary<string, string> {
            { cookie1, value1 },
            { cookie2, value2 }
        };

        // Act
        context.SetupRequestCookies(cookies);

        // Assert
        context.Request.Cookies.Count.Should().Be(cookies.Count);
        context.Request.Cookies.Should().HaveCount(cookies.Count);
        context.Request.Cookies.Should().BeEquivalentTo(cookies);
        foreach (var item in cookies)
        {
            context.Request.Cookies.ContainsKey(item.Key).Should().BeTrue();
            context.Request.Cookies.TryGetValue(item.Key, out var value).Should().BeTrue();
            context.Request.Cookies[item.Key].Should().BeEquivalentTo(item.Value);
            value.Should().BeEquivalentTo(item.Value);
        }

        context.Request.Cookies["notexist"].Should().BeEquivalentTo(null);
        context.Request.Cookies.ContainsKey("notexist").Should().BeFalse();
        context.Request.Cookies.TryGetValue("notexist", out var valueNotExist).Should().BeFalse();
        valueNotExist.Should().BeNull();
    }

    [Fact]
    public void SetupRequestMethod_WhenValue_ShouldSetMethod()
    {
        // Arrange
        var context = new HttpContextMock();

        // Act
        context.SetupRequestMethod(HttpMethods.Post);

        // Assert
        context.Request.Method.Should().Be(HttpMethods.Post);
    }

    [Fact]
    public void SetupRequestContentLength_WhenValue_ShouldSetContentLength()
    {
        // Arrange
        var context = new HttpContextMock();

        // Act
        context.SetupRequestContentLength(123);

        // Assert
        context.Request.ContentLength.Should().Be(123);
    }

    [Fact]
    public void SetupRequestContentType_WhenValue_ShouldSetContentType()
    {
        // Arrange
        var context = new HttpContextMock();

        // Act
        context.SetupRequestContentType("application/json");

        // Assert
        context.Request.ContentType.Should().Be("application/json");
    }

    [Fact]
    public void SetupResponseHeaders_WhenEmpty_ShouldNoHeaders()
    {
        // Arrange
        var context = new HttpContextMock();
        var headers = new Dictionary<string, StringValues>();

        // Act
        context.SetupResponseHeaders(headers);

        // Assert
        context.Response.Headers.Count.Should().Be(0);
        context.Response.Headers.Should().HaveCount(0);
    }

    [Fact]
    public void SetupResponseHeaders_WhenHaveHeaders_ShouldHaveHeaders()
    {
        // Arrange
        const string header1 = "header1", header2 = "header2", value1 = "value1", value2 = "value2";
        var context = new HttpContextMock();
        var headers = new Dictionary<string, StringValues> {
            { header1, value1 },
            { header2, value2 }
        };

        // Act
        context.SetupResponseHeaders(headers);

        // Assert
        context.Response.Headers.Count.Should().Be(headers.Count);
        context.Response.Headers.Should().HaveCount(headers.Count);
        context.Response.Headers.Should().BeEquivalentTo(headers);
        foreach (var item in headers)
        {
            context.Response.Headers[item.Key].Should().BeEquivalentTo(item.Value);
            context.Response.Headers.ContainsKey(item.Key).Should().BeTrue();
            context.Response.Headers.Contains(new KeyValuePair<string, StringValues>(item.Key, item.Value)).Should().BeTrue();
            context.Response.Headers.TryGetValue(item.Key, out var value).Should().BeTrue();
            value.Should().BeEquivalentTo(item.Value);
        }

        context.Response.Headers["notexist"].Should().BeEquivalentTo(StringValues.Empty);
        context.Response.Headers.ContainsKey("notexist").Should().BeFalse();
        context.Response.Headers.Contains(new KeyValuePair<string, StringValues>("notexist", "notexist")).Should().BeFalse();
        context.Response.Headers.TryGetValue("notexist", out var valueNotExist).Should().BeFalse();
        valueNotExist.Should().BeEmpty();
    }

    [Fact]
    public void SetupResponseContentLength_WhenValue_ShouldSetContentLength()
    {
        // Arrange
        var context = new HttpContextMock();

        // Act
        context.SetupResponseContentLength(123);

        // Assert
        context.Response.ContentLength.Should().Be(123);
    }

    [Fact]
    public void SetupResponseContentType_WhenValue_ShouldSetContentType()
    {
        // Arrange
        var context = new HttpContextMock();

        // Act
        context.SetupResponseContentType("application/json");

        // Assert
        context.Response.ContentType.Should().Be("application/json");
    }

    [Fact]
    public void SetupResponseStatusCode_WhenValue_ShouldSetStatusCode()
    {
        // Arrange
        var context = new HttpContextMock();

        // Act
        context.SetupResponseStatusCode(System.Net.HttpStatusCode.PartialContent);

        // Assert
        context.Response.StatusCode.Should().Be(206);
    }

    [Fact]
    public void SetupResponseBody_WhenValue_ShouldSetBody()
    {
        // Arrange
        var context = new HttpContextMock();
        var body = new MemoryStream([0x1, 0x2, 0x3]);

        // Act
        context.SetupResponseBody(body);

        // Assert
        var data = new MemoryStream();
        context.Response.Body.CopyTo(data);

        data.ToArray().Should().BeEquivalentTo([0x1, 0x2, 0x3]);
    }

    [Fact]
    public async Task SetupResponseBody_WhenValue_ShouldSetBodyWriter()
    {
        // Arrange
        var context = new HttpContextMock();
        var body = new MemoryStream();
        body.Write([0x1, 0x2, 0x3]);

        // Act
        context.SetupResponseBody(body);

        // Write to the PipeWriter
        var writer = context.Response.BodyWriter;
        await writer.WriteAsync(new ReadOnlyMemory<byte>([0x4]));
        await writer.CompleteAsync();

        // Assert
        var data = new MemoryStream();
        context.Response.Body.Position = 0; // Reset the position to read from the beginning
        await context.Response.Body.CopyToAsync(data);

        data.ToArray().Should().BeEquivalentTo(new byte[] { 0x1, 0x2, 0x3, 0x4 });
    }

    [Fact]
    public void SetupSession_WhenIfNotCalled_ShouldNotHaveSession()
    {
        // Arrange
        var context = new HttpContextMock();

        // Assert
        Assert.Throws<InvalidOperationException>(() => context.SessionMock);
        Assert.Throws<InvalidOperationException>(() => context.Session);
        context.Features.Get<ISessionFeature>().Should().BeNull();
    }

    [Fact]
    public void SetupSession_WhenIfCalled_ShouldHaveSession()
    {
        // Arrange
        var context = new HttpContextMock();

        // Act
        context.SetupSession();

        // Assert
        context.SessionMock.Should().NotBeNull();
        context.Session.Should().NotBeNull();
        context.Features.Get<ISessionFeature>().Should().NotBeNull();
    }
}
