using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests
{
    public class HttpContextMockTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void HttpContextMock_WhenRun_AssertTrue(UnitTest<HttpContextMock> unitTest)
        {
            unitTest.Run(() => new HttpContextMock());
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<HttpContextMock>[]
            {
                //Class
                new ContextMockUnitTest<HttpContextMock, HttpContext>(),
                new AssertUnitTest<HttpContextMock>(
                    t => t.RequestMock.Should().NotBeNull(),
                    t => t.Mocks.Get<HttpRequestMock>().Should().NotBeNull(),
                    t => t.ResponseMock.Should().NotBeNull(),
                    t => t.Mocks.Get<HttpResponseMock>().Should().NotBeNull(),
                    t => t.FeaturesMock.Should().NotBeNull(),
                    t => t.Mocks.Get<FeatureCollectionMock>().Should().NotBeNull(),
                    t => t.ConnectionMock.Should().NotBeNull(),
                    t => t.Mocks.Get<ConnectionInfoMock>().Should().NotBeNull(),
                    t => t.ItemsMock.Should().NotBeNull(),
                    t => t.Mocks.Get<ItemsDictionaryMock>().Should().NotBeNull(),
                    t => t.UserMock.Should().NotBeNull(),
                    t => t.Mocks.Get<ClaimsPrincipalMock>().Should().NotBeNull(),
                    t => t.RequestServicesMock.Should().NotBeNull(),
                    t => t.Mocks.Get<ServiceProviderMock>().Should().NotBeNull()
                ),
                //Properties
                // Request
                new FuncAndAssertResultUnitTest<HttpContextMock, HttpRequestMock>(
                    t => t.RequestMock = new HttpRequestMock(t),
                    (t, v) => t.RequestMock.Should().BeSameAs(v),
                    (t, v) => t.Request.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<HttpRequestMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpContextMock, HttpContext, HttpRequest>(
                    t => t.Request, Times.Never
                ),
                // Response
                new FuncAndAssertResultUnitTest<HttpContextMock, HttpResponseMock>(
                    t => t.ResponseMock = new HttpResponseMock(t),
                    (t, v) => t.ResponseMock.Should().BeSameAs(v),
                    (t, v) => t.Response.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<HttpResponseMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpContextMock, HttpContext, HttpResponse>(
                    t => t.Response, Times.Never
                ),
                // Features
                new FuncAndAssertResultUnitTest<HttpContextMock, FeatureCollectionMock>(
                    t => t.FeaturesMock = new FeatureCollectionMock(),
                    (t, v) => t.FeaturesMock.Should().BeSameAs(v),
                    (t, v) => t.Features.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<FeatureCollectionMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpContextMock, HttpContext, IFeatureCollection>(
                    t => t.Features, Times.Never
                ),
                // Connection
                new FuncAndAssertResultUnitTest<HttpContextMock, ConnectionInfoMock>(
                    t => t.ConnectionMock = new ConnectionInfoMock(),
                    (t, v) => t.ConnectionMock.Should().BeSameAs(v),
                    (t, v) => t.Connection.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<ConnectionInfoMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpContextMock, HttpContext, ConnectionInfo>(
                    t => t.Connection, Times.Never
                ),
                // Items
                new FuncAndAssertResultUnitTest<HttpContextMock, IItemsDictionaryMock>(
                    t => t.ItemsMock = new ItemsDictionaryMock(),
                    (t, v) => t.ItemsMock.Should().BeSameAs(v),
                    (t, v) => t.Items.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<ItemsDictionaryMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpContextMock, HttpContext, IDictionary<object, object>>(
                    t => t.Items, Times.Never
                ),
                new FuncAndAssertResultUnitTest<HttpContextMock, IDictionary<object, object>>(
                    t => t.Items = new Dictionary<object, object>(),
                    (t, v) => t.ItemsMock.Should().BeNull(),
                    (t, v) => t.Items.Should().BeSameAs(v)
                ),
                // Session
                new FuncAndAssertResultUnitTest<HttpContextMock, SessionMock>(
                    t => t.SessionMock = new SessionMock(),
                    (t, v) => t.SessionMock.Should().BeSameAs(v),
                    (t, v) => t.Session.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<SessionMock>().Should().BeSameAs(v)
                ),
                new FuncAndAssertResultUnitTest<HttpContextMock, ISession>(
                    t => t.Session = new Mock<ISession>().Object,
                    (t, v) => t.SessionMock.Should().BeNull(),
                    (t, v) => t.Session.Should().BeSameAs(v)
                ),
                new ActionAndAssertUnitTest<HttpContextMock>(
                    t => t.Session = null,
                    t => Assert.Throws<InvalidOperationException>(() => t.Session)
                ),
                // User
                new FuncAndAssertResultUnitTest<HttpContextMock, ClaimsPrincipalMock>(
                    t => t.UserMock = new ClaimsPrincipalMock(),
                    (t, v) => t.UserMock.Should().BeSameAs(v),
                    (t, v) => t.User.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<ClaimsPrincipalMock>().Should().BeSameAs(v)
                ),
                new FuncAndAssertResultUnitTest<HttpContextMock, ClaimsPrincipal>(
                    t => t.User = new ClaimsPrincipal(),
                    (t, v) => t.UserMock.Should().BeNull(),
                    (t, v) => t.User.Should().BeSameAs(v)
                ),
                // RequestServices
                new FuncAndAssertResultUnitTest<HttpContextMock, ServiceProviderMock>(
                    t => t.RequestServicesMock = new ServiceProviderMock(),
                    (t, v) => t.RequestServicesMock.Should().BeSameAs(v),
                    (t, v) => t.RequestServices.Should().BeSameAs(v),
                    (t, v) => t.Mocks.Get<ServiceProviderMock>().Should().BeSameAs(v)
                ),
                new PropertyGetUnitTest<HttpContextMock, HttpContext, IServiceProvider>(
                    t => t.RequestServices, Times.Never
                ),
                new FuncAndAssertResultUnitTest<HttpContextMock, IServiceProvider>(
                    t => t.RequestServices = new ServiceCollection().BuildServiceProvider(),
                    (t, v) => t.RequestServicesMock.Should().BeNull(),
                    (t, v) => t.RequestServices.Should().BeSameAs(v)
                ),
                // RequestAborted
                new PropertyGetSetUnitTest<HttpContextMock, HttpContext, CancellationToken>(
                    t => t.RequestAborted,
                    t => t.RequestAborted = Fakes.CancellationToken
                ),
                // TraceIdentifier
                new PropertyGetSetUnitTest<HttpContextMock, HttpContext, string>(
                    t => t.TraceIdentifier,
                    t => t.TraceIdentifier = Fakes.String
                ),
                // WebSockets
                new PropertyGetUnitTest<HttpContextMock, HttpContext, WebSocketManager>(
                    t => t.WebSockets
                ),
                //Methods
                new MethodInvokeUnitTest<HttpContextMock, HttpContext>(
                    t => t.Abort()
                ),
            }.ToData();
    }
}
