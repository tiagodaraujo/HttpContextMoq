using System.Collections;
using System.Collections.Generic;
using HttpContextMoq.Generic;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HttpContextMoq
{
    public class RequestCookieCollectionMock : IRequestCookieCollection, IContextMock<IRequestCookieCollection>
    {
        public RequestCookieCollectionMock()
        {
            this.Mock = new Mock<IRequestCookieCollection>();
        }

        public Mock<IRequestCookieCollection> Mock { get; }

        public string this[string key] => this.Mock.Object[key];

        public int Count => this.Mock.Object.Count;

        public ICollection<string> Keys => this.Mock.Object.Keys;

        public bool ContainsKey(string key) => this.Mock.Object.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, string>> GetEnumerator() => this.Mock.Object.GetEnumerator();

        public bool TryGetValue(string key, out string value) => this.Mock.Object.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.Mock.Object).GetEnumerator();
    }
}