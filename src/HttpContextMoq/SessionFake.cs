namespace HttpContextMoq;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class SessionFake : ISession
{
    private readonly Dictionary<string, byte[]> data = new(StringComparer.OrdinalIgnoreCase);

    public bool IsAvailable => true;

    public string Id { get; } = Guid.NewGuid().ToString();

    public IEnumerable<string> Keys => this.data.Keys;

    public void Clear() => this.data.Clear();

    public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public void Remove(string key) => this.data.Remove(key);

    public void Set(string key, byte[] value) => this.data.Add(key, value);

    public bool TryGetValue(string key, out byte[] value) => this.data.TryGetValue(key, out value);
}
