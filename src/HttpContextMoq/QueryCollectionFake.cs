using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace HttpContextMoq;

public class QueryCollectionFake : IQueryCollection
{
    private readonly Dictionary<string, StringValues> _query;

    public QueryCollectionFake(Dictionary<string, StringValues> query)
    {
        _query = new(query, StringComparer.OrdinalIgnoreCase);
    }

    public StringValues this[string key]
    {
        get
        {
            if (TryGetValue(key, out var value))
            {
                return value;
            }

            return StringValues.Empty;
        }
    }

    public int Count => _query.Count;

    public ICollection<string> Keys => _query.Keys;

    public bool ContainsKey(string key) => _query.ContainsKey(key);

    public bool TryGetValue(string key, out StringValues value) => _query.TryGetValue(key, out value);

    public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator() => _query.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => _query.GetEnumerator();
}
