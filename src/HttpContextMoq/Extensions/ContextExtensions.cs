using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
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

            httpContextMock.RequestMock.Mock.Setup(x => x.Protocol).Returns("HTTP/1.1");
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

        public static HttpContextMock SetupRequestMethod(this HttpContextMock httpContextMock, string method)
        {
            httpContextMock.RequestMock.Mock.Setup(x => x.Method).Returns(method);

            return httpContextMock;
        }

        public static HttpContextMock SetupRequestBody(this HttpContextMock httpContextMock, Stream stream)
        {
            httpContextMock.RequestMock.Mock.Setup(x => x.Body).Returns(stream);

            return httpContextMock;
        }

        public static HttpContextMock SetupRequestContentType(this HttpContextMock httpContextMock, string contentType)
        {
            httpContextMock.RequestMock.Mock.Setup(x => x.ContentType).Returns(contentType);

            return httpContextMock;
        }

        public static HttpContextMock SetupRequestContentLength(this HttpContextMock httpContextMock, long? contentLength)
        {
            httpContextMock.RequestMock.Mock.Setup(x => x.ContentLength).Returns(contentLength);

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


        public static HttpContextMock SetupResponseStatusCode(this HttpContextMock httpContextMock, HttpStatusCode statusCode) => SetupResponseStatusCode(httpContextMock, (int) statusCode);

        public static HttpContextMock SetupResponseStatusCode(this HttpContextMock httpContextMock, int statusCode)
        {
            httpContextMock.ResponseMock.Mock.Setup(x => x.StatusCode).Returns(statusCode);

            return httpContextMock;
        }

        public static HttpContextMock SetupResponseBody(this HttpContextMock httpContextMock, Stream stream)
        {
            httpContextMock.ResponseMock.Mock.Setup(x => x.Body).Returns(stream);

            return httpContextMock;
        }

        public static HttpContextMock SetupResponseContentType(this HttpContextMock httpContextMock, string contentType)
        {
            httpContextMock.ResponseMock.Mock.Setup(x => x.ContentType).Returns(contentType);

            return httpContextMock;
        }

        public static HttpContextMock SetupResponseHeaders(this HttpContextMock httpContextMock, IDictionary<string, StringValues> headers)
        {
            httpContextMock.ResponseMock.SetHeaders(new HeaderDictionaryFake(headers));

            return httpContextMock;
        }

        public static HttpContextMock SetupResponseContentLength(this HttpContextMock httpContextMock, long? contentLength)
        {
            httpContextMock.ResponseMock.Mock.Setup(x => x.ContentLength).Returns(contentLength);

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
