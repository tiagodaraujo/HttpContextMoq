using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace HttpContextMoq;

public class SessionFake : ISession
{
    private readonly Dictionary<string, byte[]> _dictionary = new(StringComparer.OrdinalIgnoreCase);

    public bool IsAvailable => true;

    public string Id { get; } = Guid.NewGuid().ToString();

    public IEnumerable<string> Keys => _dictionary.Keys;

    public void Clear() => _dictionary.Clear();

    public Task CommitAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public Task LoadAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;

    public void Remove(string key) => _dictionary.Remove(key);

    public void Set(string key, byte[] value) => _dictionary.Add(key, value);

    public bool TryGetValue(string key, out byte[] value) => _dictionary.TryGetValue(key, out value);
}
