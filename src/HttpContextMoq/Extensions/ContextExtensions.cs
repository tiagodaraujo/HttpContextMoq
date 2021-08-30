using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;
using Moq;

namespace HttpContextMoq.Extensions
{
    public static class ContextExtensions
    {
        public static HttpContextMock SetupUrl(this HttpContextMock httpContextMock, string url)
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
            httpContextMock.RequestMock.Query = new QueryCollectionFake(queryDictionary);

            var requestFeature = new Mock<IHttpRequestFeature>();
            requestFeature.Setup(x => x.RawTarget).Returns(uri.PathAndQuery);
            httpContextMock.FeaturesMock.Mock.Setup(x => x.Get<IHttpRequestFeature>()).Returns(requestFeature.Object);

            return httpContextMock;
        }

        public static HttpContextMock SetupRequestHeaders(this HttpContextMock httpContextMock, IDictionary<string, StringValues> headers)
        {
            httpContextMock.RequestMock.SetHeaders(new HeaderDictionaryFake(headers));

            return httpContextMock;
        }

        public static HttpContextMock SetupRequestCookies(this HttpContextMock httpContextMock, IDictionary<string, string> cookies)
        {
            httpContextMock.RequestMock.Cookies = new RequestCookieCollectionFake(cookies);

            return httpContextMock;
        }

        public static HttpContextMock SetupSession(this HttpContextMock httpContextMock)
        {
            var session = new SessionMock();
            httpContextMock.SessionMock = session;
            httpContextMock.FeaturesMock.Mock.Setup(x => x.Get<ISessionFeature>()).Returns(new SessionFeatureFake() { Session = session });

            return httpContextMock;
        }

        public static HttpContextMock SetupRequestService<TService>(this HttpContextMock httpContextMock, TService instance)
        {
            httpContextMock.RequestServicesMock.Mock.Setup(x => x.GetService(typeof(TService))).Returns(instance);

            return httpContextMock;
        }

        public static HttpContextMock SetupRequestService<TService>(this HttpContextMock httpContextMock, Func<TService> factory)
        {
            httpContextMock.RequestServicesMock.Mock.Setup(x => x.GetService(typeof(TService))).Returns(() => (object)factory());

            return httpContextMock;
        }
    }
}
