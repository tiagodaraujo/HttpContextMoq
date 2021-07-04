namespace HttpContextMoq.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Web;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.Extensions.Primitives;
    using Moq;

    public static class ContextExtensions
    {
        public static void SetupUrl(this HttpContextMock httpContextMock, string url)
        {
            var uri = new Uri(url);

            httpContextMock.RequestMock.Mock.Setup(x => x.IsHttps).Returns(uri.Scheme == "https");
            httpContextMock.RequestMock.Mock.Setup(x => x.Scheme).Returns(uri.Scheme);
            if ((uri.Scheme == "https" && uri.Port != 443) || (uri.Scheme == "http" && uri.Port != 80))
            {
                httpContextMock.RequestMock.Mock.Setup(x => x.Host).Returns(new HostString(uri.Host, uri.Port));
            }
            else
            {
                httpContextMock.RequestMock.Mock.Setup(x => x.Host).Returns(new HostString(uri.Host));
            }

            httpContextMock.RequestMock.Mock.Setup(x => x.PathBase).Returns(string.Empty);
            httpContextMock.RequestMock.Mock.Setup(x => x.Path).Returns(uri.AbsolutePath);
            httpContextMock.RequestMock.Mock.Setup(x => x.QueryString).Returns(new QueryString(uri.Query));

            var query = HttpUtility.ParseQueryString(uri.Query);
            var paramQuery = new Dictionary<string, StringValues>();
            foreach (var key in query.AllKeys)
            {
                paramQuery.Add(key, query[key]);
            }
            httpContextMock.RequestMock.Query = new QueryCollection(paramQuery);

            var requestFeature = new Mock<IHttpRequestFeature>();
            requestFeature.Setup(x => x.RawTarget).Returns(uri.PathAndQuery);
            httpContextMock.FeaturesMock.Mock.Setup(x => x.Get<IHttpRequestFeature>()).Returns(requestFeature.Object);
        }

        public static void SetupSession(this HttpContextMock httpContextMock)
        {
            var session = new SessionMock();
            httpContextMock.SessionMock = session;
            httpContextMock.FeaturesMock.Mock.Setup(x => x.Get<ISessionFeature>()).Returns(new DefaultSessionFeature() { Session = session });
        }
    }
}
