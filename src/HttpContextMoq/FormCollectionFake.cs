using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace HttpContextMoq;

public class FormCollectionFake : IFormCollection
{
    private readonly Dictionary<string, StringValues> _fields;
    private readonly IFormFileCollection _files;

    public FormCollectionFake()
    {
        _fields = new Dictionary<string, StringValues>();
        _files = new FormFileCollectionFake();
    }

    public FormCollectionFake(IDictionary<string, StringValues> fields, IFormFileCollection files = null)
    {
        _fields = new Dictionary<string, StringValues>(fields);
        _files = files == null ? new FormFileCollectionFake() : files;
    }

    public StringValues this[string key] => _fields[key];

    public int Count => _fields.Count;

    public ICollection<string> Keys => _fields.Keys;

    public IFormFileCollection Files => (IFormFileCollection)_files;

    public IList<IFormFile> FilesFake => (IList<IFormFile>)_files;

    public bool ContainsKey(string key) => _fields.ContainsKey(key);

    public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator() => _fields.GetEnumerator();

    public bool TryGetValue(string key, out StringValues value) => _fields.TryGetValue(key, out value);

    IEnumerator IEnumerable.GetEnumerator() => _fields.GetEnumerator();
}
