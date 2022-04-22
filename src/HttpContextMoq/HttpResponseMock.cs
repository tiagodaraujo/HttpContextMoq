using System;
using System.IO;
using System.Threading.Tasks;
using HttpContextMoq.Generic;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HttpContextMoq
{
    public class HttpResponseMock : HttpResponse, IContextMocks<HttpResponse>
    {
        private HttpContextMock _httpContextMock;
        private IHeaderDictionary _headers;
        private IResponseCookies _cookies;

        public HttpResponseMock(HttpContextMock httpContextMock)
        {
            this.Mock = new Mock<HttpResponse>();
            this.Mocks = new MockCollection(this);
            this.HttpContextMock = httpContextMock;
            this.HeadersMock = new HeaderDictionaryMock();
            this.CookiesMock = new ResponseCookiesMock();
        }

        public Mock<HttpResponse> Mock { get; }

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
                this._headers = value;
                this.Mocks.Add(value);
            }
        }

        public ResponseCookiesMock CookiesMock
        {
            get => _cookies as ResponseCookiesMock;
            set
            {
                _cookies = value;
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

        public override IResponseCookies Cookies => _cookies;

        public override bool HasStarted => this.Mock.Object.HasStarted;

        public override IHeaderDictionary Headers => _headers;

        public override HttpContext HttpContext => this.HttpContextMock;

        public override int StatusCode
        {
            get => this.Mock.Object.StatusCode;
            set => this.Mock.Object.StatusCode = value;
        }

        internal void SetHeaders(IHeaderDictionary headers)
        {
            this._headers = headers;
            this.Mocks.Add(headers);
        }

        public override void OnCompleted(Func<object, Task> callback, object state) => this.Mock.Object.OnCompleted(callback, state);

        public override void OnStarting(Func<object, Task> callback, object state) => this.Mock.Object.OnStarting(callback, state);

        public override void Redirect(string location, bool permanent) => this.Mock.Object.Redirect(location, permanent);
    }
}