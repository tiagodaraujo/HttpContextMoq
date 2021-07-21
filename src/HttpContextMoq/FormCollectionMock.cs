using System.Collections;
using System.Collections.Generic;
using HttpContextMoq.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;

namespace HttpContextMoq
{
    public class FormCollectionMock : IFormCollection, IContextMocks<IFormCollection>
    {
        private IFormFileCollection _files;

        public FormCollectionMock()
        {
            this.Mock = new Mock<IFormCollection>();
            this.Mocks = new MockCollection(this);
            this.FilesMock = new FormFileCollectionMock();
        }

        public Mock<IFormCollection> Mock { get; }

        public MockCollection Mocks { get; }

        public FormFileCollectionMock FilesMock
        {
            get => _files as FormFileCollectionMock;
            set
            {
                _files = value;
                this.Mocks.Add(value);
            }
        }

        public StringValues this[string key] => this.Mock.Object[key];

        public int Count => this.Mock.Object.Count;

        public IFormFileCollection Files => _files;

        public ICollection<string> Keys => this.Mock.Object.Keys;

        public bool ContainsKey(string key) => this.Mock.Object.ContainsKey(key);

        public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator() => this.Mock.Object.GetEnumerator();

        public bool TryGetValue(string key, out StringValues value) => this.Mock.Object.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => this.Mock.Object.GetEnumerator();
    }
}