namespace HttpContextMoq
{
    using System.Collections;
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Http;
    using Moq;

    public class FormFileCollectionMock : IFormFileCollection
    {
        public FormFileCollectionMock()
        {
            this.Mock = new Mock<IFormFileCollection>();
        }

        public Mock<IFormFileCollection> Mock { get; }

        public IFormFile this[string name] => this.Mock.Object[name];

        public IFormFile this[int index] => this.Mock.Object[index];

        public int Count => this.Mock.Object.Count;

        public IEnumerator<IFormFile> GetEnumerator() => this.Mock.Object.GetEnumerator();

        public IFormFile GetFile(string name) => this.Mock.Object.GetFile(name);

        public IReadOnlyList<IFormFile> GetFiles(string name) => this.Mock.Object.GetFiles(name);

        IEnumerator IEnumerable.GetEnumerator() => this.Mock.Object.GetEnumerator();
    }
}