﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;

namespace HttpContextMoq;

public class HeaderDictionaryMock : IHeaderDictionaryMock
{
    public HeaderDictionaryMock()
    {
        this.Mock = new Mock<IHeaderDictionary>();
    }

    public Mock<IHeaderDictionary> Mock { get; }

    public StringValues this[string key]
    {
        get => this.Mock.Object[key];
        set => this.Mock.Object[key] = value;
    }

    public long? ContentLength
    {
        get => this.Mock.Object.ContentLength;
        set => this.Mock.Object.ContentLength = value;
    }

    public ICollection<string> Keys => this.Mock.Object.Keys;

    public ICollection<StringValues> Values => this.Mock.Object.Values;

    public int Count => this.Mock.Object.Count;

    public bool IsReadOnly => this.Mock.Object.IsReadOnly;

    [SuppressMessage("Usage", "ASP0019:Suggest using IHeaderDictionary.Append or the indexer", Justification = "<Pending>")]
    public void Add(string key, StringValues value) => this.Mock.Object.Add(key, value);

    public void Add(KeyValuePair<string, StringValues> item) => this.Mock.Object.Add(item);

    public void Clear() => this.Mock.Object.Clear();

    public bool Contains(KeyValuePair<string, StringValues> item) => this.Mock.Object.Contains(item);

    public bool ContainsKey(string key) => this.Mock.Object.ContainsKey(key);

    public void CopyTo(KeyValuePair<string, StringValues>[] array, int arrayIndex) => this.Mock.Object.CopyTo(array, arrayIndex);

    public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator() => this.Mock.Object.GetEnumerator();

    public bool Remove(string key) => this.Mock.Object.Remove(key);

    public bool Remove(KeyValuePair<string, StringValues> item) => this.Mock.Object.Remove(item);

    public bool TryGetValue(string key, out StringValues value) => this.Mock.Object.TryGetValue(key, out value);

    IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.Mock.Object).GetEnumerator();
}