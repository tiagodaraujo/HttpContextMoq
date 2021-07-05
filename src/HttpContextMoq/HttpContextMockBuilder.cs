using HttpContextMoq.Extensions;

namespace HttpContextMoq
{
    public class HttpContextMockBuilder
    {
        private string url;
        private bool hasSession;

        public HttpContextMockBuilder WithUrl(string url)
        {
            this.url = url;
            return this;
        }

        public HttpContextMockBuilder WithSession()
        {
            this.hasSession = true;
            return this;
        }

        public HttpContextMock Build()
        {
            var context = new HttpContextMock();

            if (!string.IsNullOrEmpty(this.url))
            {
                context.SetupUrl(this.url);
            }

            if (hasSession)
            {
                context.SetupSession();
            }

            return context;
        }

        public static HttpContextMockBuilder Create(string url = null)
        {
            return new HttpContextMockBuilder()
                .WithUrl(url);
        }
    }
}
