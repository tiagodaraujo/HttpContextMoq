using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Moq;

namespace HttpContextMoq.Extensions
{
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

            var queryString = QueryString.FromUriComponent(uri);
            httpContextMock.RequestMock.Mock.Setup(x => x.QueryString).Returns(queryString);

            var queryDictionary = QueryHelpers.ParseQuery(queryString.ToString());
            httpContextMock.RequestMock.Query = new QueryCollection(queryDictionary);

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
