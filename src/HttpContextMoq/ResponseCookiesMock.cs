namespace HttpContextMoq
{
    using HttpContextMoq.Generic;
    using Microsoft.AspNetCore.Http;
    using Moq;

    public class ResponseCookiesMock : IResponseCookies, IContextMock<IResponseCookies>
    {
        public ResponseCookiesMock()
        {
            this.Mock = new Mock<IResponseCookies>();
        }

        public Mock<IResponseCookies> Mock { get; }

        public void Append(string key, string value) => this.Mock.Object.Append(key, value);

        public void Append(string key, string value, CookieOptions options) => this.Mock.Object.Append(key, value, options);

        public void Delete(string key) => this.Mock.Object.Delete(key);

        public void Delete(string key, CookieOptions options) => this.Mock.Object.Delete(key, options);
    }
}