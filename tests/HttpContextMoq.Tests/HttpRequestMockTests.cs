using System.Collections.Generic;
using System.IO;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests
{
    public class HttpRequestMockTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void HttpRequestMock_WhenRun_AssertTrue(UnitTest<HttpRequestMock> unitTest)
        {
            unitTest.Run(() => new HttpRequestMock(new HttpContextMock()));
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<HttpRequestMock>[]
            {
                //Class
                new ContextMockUnitTest<HttpRequestMock, HttpRequest>(),
                new AssertUnitTest<HttpRequestMock>(
                    t => t.HttpContextMock.Should().NotBeNull(),
                    t => t.Mocks.Get<HttpContextMock>().Should().NotBeNull(),
                    t => t.HeadersMock.Should().NotBeNull(),
                    t => t.Mocks.Get<HeaderDictionaryMock>().Should().NotBeNull(),
                    t => t.QueryMock.Should().NotBeNull(),
                    t => t.Mocks.Get<QueryCollectionMock>().Should().NotBeNull(),
                    t => t.CookiesMock.Should().NotBeNull(),
                    t => t.Mocks.Get<RequestCookieCollectionMock>().Should().NotBeNull(),
                    t => t.FormMock.Should().NotBeNull(),
                    t => t.Mocks.Get<FormCollectionMock>().Should().NotBeNull(),
                    t => t.RouteValues.Should().NotBeNull()
                ),
                // HttpContext
                new FuncAndAssertResultUnitTest<HttpRequestMock, HttpContextMock>(
                    t => t.HttpContextMock = new HttpContextMock(),
                    (t, v) => t.HttpContextMock.Should().BeSameAs(v),
                    (t, v) => t.HttpContext.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<HttpContextMock>().Should().BeSameAs(v)
                ),
                // Headers
                new FuncAndAssertResultUnitTest<HttpRequestMock, IHeaderDictionaryMock>(
                    t => t.HeadersMock = new HeaderDictionaryMock(),
                    (t, v) => t.HeadersMock.Should().BeSameAs(v),
                    (t, v) => t.Headers.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<HeaderDictionaryMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpRequestMock, HttpRequest, IHeaderDictionary>(
                    t => t.Headers, Times.Never
                ),
                // Query
                new FuncAndAssertResultUnitTest<HttpRequestMock, QueryCollectionMock>(
                    t => t.QueryMock = new QueryCollectionMock(),
                    (t, v) => t.QueryMock.Should().BeSameAs(v),
                    (t, v) => t.Query.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<QueryCollectionMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpRequestMock, HttpRequest, IQueryCollection>(
                    t => t.Query, Times.Never
                ),
                new FuncAndAssertResultUnitTest<HttpRequestMock, IQueryCollection>(
                    t => t.Query = new Mock<IQueryCollection>().Object,
                    (t, v) => t.QueryMock.Should().BeNull(),
                    (t, v) => t.Query.Should().BeSameAs(v)
                ),
                // Cookies
                new FuncAndAssertResultUnitTest<HttpRequestMock, RequestCookieCollectionMock>(
                    t => t.CookiesMock = new RequestCookieCollectionMock(),
                    (t, v) => t.CookiesMock.Should().BeSameAs(v),
                    (t, v) => t.Cookies.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<RequestCookieCollectionMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpRequestMock, HttpRequest, IRequestCookieCollection>(
                    t => t.Cookies, Times.Never
                ),
                new FuncAndAssertResultUnitTest<HttpRequestMock, IRequestCookieCollection>(
                    t => t.Cookies = new Mock<IRequestCookieCollection>().Object,
                    (t, v) => t.CookiesMock.Should().BeNull(),
                    (t, v) => t.Cookies.Should().BeSameAs(v)
                ),
                // Form
                new FuncAndAssertResultUnitTest<HttpRequestMock, FormCollectionMock>(
                    t => t.FormMock = new FormCollectionMock(),
                    (t, v) => t.FormMock.Should().BeSameAs(v),
                    (t, v) => t.Form.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<FormCollectionMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpRequestMock, HttpRequest, IFormCollection>(
                    t => t.Form, Times.Never
                ),
                new FuncAndAssertResultUnitTest<HttpRequestMock, IFormCollection>(
                    t => t.Form = new Mock<IFormCollection>().Object,
                    (t, v) => t.FormMock.Should().BeNull(),
                    (t, v) => t.Form.Should().BeSameAs(v)
                ),
                //Properties
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, Stream>(
                    t => t.Body,
                    t => t.Body = It.IsAny<Stream>()
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, long?>(
                    t => t.ContentLength,
                    t => t.ContentLength = Fakes.Long
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, string>(
                    t => t.ContentType,
                    t => t.ContentType = Fakes.String
                ),
                new PropertyGetUnitTest<HttpRequestMock, HttpRequest, bool>(
                    t => t.HasFormContentType
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, HostString>(
                    t => t.Host,
                    t => t.Host = new HostString(Fakes.String)
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, bool>(
                    t => t.IsHttps,
                    t => t.IsHttps = Fakes.Bool
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, string>(
                    t => t.Method,
                    t => t.Method = Fakes.String
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, PathString>(
                    t => t.Path,
                    t => t.Path = $"/{Fakes.String}"
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, PathString>(
                    t => t.PathBase,
                    t => t.PathBase = $"/{Fakes.String}"
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, string>(
                    t => t.Protocol,
                    t => t.Protocol = Fakes.String
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, QueryString>(
                    t => t.QueryString,
                    t => t.QueryString = new QueryString($"?{Fakes.String}")
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, string>(
                    t => t.Scheme,
                    t => t.Scheme = Fakes.String
                ),
                new PropertyGetSetUnitTest<HttpRequestMock, HttpRequest, RouteValueDictionary>(
                    t => t.RouteValues,
                    t => t.RouteValues = new RouteValueDictionary(),
                    Times.Never
                ),
                //Methods
                new MethodInvokeUnitTest<HttpRequestMock, HttpRequest>(
                    t => t.ReadFormAsync(Fakes.CancellationToken)
                ),
            }.ToData();
    }
}
