using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests
{
    public class HttpResponseMockTests
    {
        //public static HttpContextMock httpContextMock = ;

        [Theory]
        [MemberData(nameof(Data))]
        public void HttpResponseMock_WhenRun_AssertTrue(UnitTest<HttpResponseMock> unitTest)
        {
            unitTest.Run(() => new HttpResponseMock(new HttpContextMock()));
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<HttpResponseMock>[]
            {
                //Class
                new ContextMockUnitTest<HttpResponseMock, HttpResponse>(),
                new AssertUnitTest<HttpResponseMock>(
                    t => t.HttpContextMock.Should().NotBeNull(),
                    t => t.Mocks.Get<HttpContextMock>().Should().NotBeNull(),
                    t => t.HeadersMock.Should().NotBeNull(),
                    t => t.Mocks.Get<HeaderDictionaryMock>().Should().NotBeNull(),
                    t => t.CookiesMock.Should().NotBeNull(),
                    t => t.Mocks.Get<ResponseCookiesMock>().Should().NotBeNull()
                ),
                // HttpContext
                new FuncAndAssertResultUnitTest<HttpResponseMock, HttpContextMock>(
                    t => t.HttpContextMock = new HttpContextMock(),
                    (t, v) => t.HttpContextMock.Should().BeSameAs(v),
                    (t, v) => t.HttpContext.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<HttpContextMock>().Should().BeSameAs(v)
                ),
                // Headers
                new FuncAndAssertResultUnitTest<HttpResponseMock, IHeaderDictionaryMock>(
                    t => t.HeadersMock = new HeaderDictionaryMock(),
                    (t, v) => t.HeadersMock.Should().BeSameAs(v),
                    (t, v) => t.Headers.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<HeaderDictionaryMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpResponseMock, HttpResponse, IHeaderDictionary>(
                    t => t.Headers, Times.Never
                ),
                // Cookies
                new FuncAndAssertResultUnitTest<HttpResponseMock, ResponseCookiesMock>(
                    t => t.CookiesMock = new ResponseCookiesMock(),
                    (t, v) => t.CookiesMock.Should().BeSameAs(v),
                    (t, v) => t.Cookies.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<ResponseCookiesMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpResponseMock, HttpResponse, IResponseCookies>(
                    t => t.Cookies, Times.Never
                ),
                //Properties
                new PropertyGetSetUnitTest<HttpResponseMock, HttpResponse, Stream>(
                    t => t.Body,
                    t => t.Body = It.IsAny<Stream>()
                ),
                new PropertyGetSetUnitTest<HttpResponseMock, HttpResponse, long?>(
                    t => t.ContentLength,
                    t => t.ContentLength = Fakes.Long
                ),
                new PropertyGetSetUnitTest<HttpResponseMock, HttpResponse, string>(
                    t => t.ContentType,
                    t => t.ContentType = Fakes.String
                ),
                new PropertyGetUnitTest<HttpResponseMock, HttpResponse, bool>(
                    t => t.HasStarted
                ),
                new PropertyGetSetUnitTest<HttpResponseMock, HttpResponse, int>(
                    t => t.StatusCode,
                    t => t.StatusCode = Fakes.Int
                ),
                //Methods
                new MethodInvokeUnitTest<HttpResponseMock, HttpResponse>(
                    t => t.OnCompleted(It.IsAny<Func<Task>>())
                ),
                new MethodInvokeUnitTest<HttpResponseMock, HttpResponse>(
                    t => t.OnCompleted(It.IsAny<Func<object, Task>>(), It.IsAny<object>())
                ),
                new MethodInvokeUnitTest<HttpResponseMock, HttpResponse>(
                    t => t.OnStarting(It.IsAny<Func<Task>>())
                ),
                new MethodInvokeUnitTest<HttpResponseMock, HttpResponse>(
                    t => t.OnStarting(It.IsAny<Func<object, Task>>(), It.IsAny<object>())
                ),
                new MethodInvokeUnitTest<HttpResponseMock, HttpResponse>(
                    t => t.Redirect(Fakes.String)
                ),
                new MethodInvokeUnitTest<HttpResponseMock, HttpResponse>(
                    t => t.Redirect(Fakes.String, Fakes.Bool)
                ),
                new MethodInvokeUnitTest<HttpResponseMock, HttpResponse>(
                    t => t.RegisterForDispose(It.IsAny<IDisposable>())
                )
            }.ToData();
    }
}
