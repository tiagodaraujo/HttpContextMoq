using System.IO;
using System.Threading;
using System.Threading.Tasks;
using HttpContextMoq.Generic;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HttpContextMoq
{
    public class HttpRequestMock : HttpRequest, IContextMocks<HttpRequest>
    {
        private HttpContextMock _httpContextMock;
        private IHeaderDictionary _headers;

        public HttpRequestMock(HttpContextMock httpContextMock)
        {
            this.Mock = new Mock<HttpRequest>();
            this.Mocks = new MockCollection(this);
            this.HttpContextMock = httpContextMock;
            this.HeadersMock = new HeaderDictionaryMock();
            this.QueryMock = new QueryCollectionMock();
            this.CookiesMock = new RequestCookieCollectionMock();
            this.FormMock = new FormCollectionMock();
        }

        public Mock<HttpRequest> Mock { get; }

        public MockCollection Mocks { get; }

        public HttpContextMock HttpContextMock
        {
            get => _httpContextMock;
            set
            {
                this._httpContextMock = value;
                this.Mocks.Add(value);
            }
        }

        public HeaderDictionaryMock HeadersMock
        {
            get => _headers as HeaderDictionaryMock;
            set
            {
                SetHeaders(value);
            }
        }

        public QueryCollectionMock QueryMock
        {
            get => this.Query as QueryCollectionMock;
            set
            {
                this.Query = value;
                this.Mocks.Add(value);
            }
        }

        public RequestCookieCollectionMock CookiesMock
        {
            get => this.Cookies as RequestCookieCollectionMock;
            set
            {
                this.Cookies = value;
                this.Mocks.Add(value);
            }
        }

        public FormCollectionMock FormMock
        {
            get => this.Form as FormCollectionMock;
            set
            {
                this.Form = value;
                this.Mocks.Add(value);
            }
        }

        public override Stream Body
        {
            get => this.Mock.Object.Body;
            set => this.Mock.Object.Body = value;
        }

        public override long? ContentLength
        {
            get => this.Mock.Object.ContentLength;
            set => this.Mock.Object.ContentLength = value;
        }

        public override string ContentType
        {
            get => this.Mock.Object.ContentType;
            set => this.Mock.Object.ContentType = value;
        }

        public override IRequestCookieCollection Cookies { get; set; }

        public override IFormCollection Form { get; set; }

        public override bool HasFormContentType => this.Mock.Object.HasFormContentType;

        public override IHeaderDictionary Headers => _headers;

        public override HostString Host
        {
            get => this.Mock.Object.Host;
            set => this.Mock.Object.Host = value;
        }

        public override HttpContext HttpContext => this.HttpContextMock;

        public override bool IsHttps
        {
            get => this.Mock.Object.IsHttps;
            set => this.Mock.Object.IsHttps = value;
        }

        public override string Method
        {
            get => this.Mock.Object.Method;
            set => this.Mock.Object.Method = value;
        }

        public override PathString Path
        {
            get => this.Mock.Object.Path;
            set => this.Mock.Object.Path = value;
        }

        public override PathString PathBase
        {
            get => this.Mock.Object.PathBase;
            set => this.Mock.Object.PathBase = value;
        }

        public override string Protocol
        {
            get => this.Mock.Object.Protocol;
            set => this.Mock.Object.Protocol = value;
        }

        public override IQueryCollection Query { get; set; }

        public override QueryString QueryString
        {
            get => this.Mock.Object.QueryString;
            set => this.Mock.Object.QueryString = value;
        }

        public override string Scheme
        {
            get => this.Mock.Object.Scheme;
            set => this.Mock.Object.Scheme = value;
        }

        public override Task<IFormCollection> ReadFormAsync(CancellationToken cancellationToken = default) => this.Mock.Object.ReadFormAsync(cancellationToken);

        internal void SetHeaders(IHeaderDictionary headers)
        {
            this._headers = headers;
            this.Mocks.Add(headers);
        }
    }
}