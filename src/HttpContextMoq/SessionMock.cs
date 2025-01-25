using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using HttpContextMoq.Generic;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HttpContextMoq;

public class SessionMock : ISession, IContextMock<ISession>
{
    public SessionMock()
    {
        this.Mock = new Mock<ISession>();
    }

    public Mock<ISession> Mock { get; }

    public string Id => this.Mock.Object.Id;

    public bool IsAvailable => this.Mock.Object.IsAvailable;

    public IEnumerable<string> Keys => this.Mock.Object.Keys;

    public void Clear() => this.Mock.Object.Clear();

    public Task CommitAsync(CancellationToken cancellationToken = default) => this.Mock.Object.CommitAsync(cancellationToken);

    public Task LoadAsync(CancellationToken cancellationToken = default) => this.Mock.Object.LoadAsync(cancellationToken);

    public void Remove(string key) => this.Mock.Object.Remove(key);

    public void Set(string key, byte[] value) => this.Mock.Object.Set(key, value);

    public bool TryGetValue(string key, out byte[] value) => this.Mock.Object.TryGetValue(key, out value);
}