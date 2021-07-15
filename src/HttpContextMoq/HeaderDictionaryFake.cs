using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace HttpContextMoq
{
    public class HeaderDictionaryFake : IHeaderDictionary
    {
        //TODO: System.NotImplementedException
        public StringValues this[string key] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public long? ContentLength { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public ICollection<string> Keys => throw new System.NotImplementedException();

        public ICollection<StringValues> Values => throw new System.NotImplementedException();

        public int Count => throw new System.NotImplementedException();

        public bool IsReadOnly => throw new System.NotImplementedException();

        public void Add(string key, StringValues value)
        {
            throw new System.NotImplementedException();
        }

        public void Add(KeyValuePair<string, StringValues> item)
        {
            throw new System.NotImplementedException();
        }

        public void Clear()
        {
            throw new System.NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, StringValues> item)
        {
            throw new System.NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new System.NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, StringValues>[] array, int arrayIndex)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new System.NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, StringValues> item)
        {
            throw new System.NotImplementedException();
        }

        public bool TryGetValue(string key, [MaybeNullWhen(false)] out StringValues value)
        {
            throw new System.NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
