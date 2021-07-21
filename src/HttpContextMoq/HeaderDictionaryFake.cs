using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.Net.Http.Headers;

namespace HttpContextMoq
{
    public class HeaderDictionaryFake : IHeaderDictionary
    {
        private readonly Dictionary<string, StringValues> _dictionary;

        public HeaderDictionaryFake()
        {
            _dictionary = new Dictionary<string, StringValues>();
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
            set
            {
                if (key == null)
                {
                    throw new ArgumentNullException(nameof(key));
                }

                ThrowIfReadOnly();
                if (value.Count == 0)
                {
                    _dictionary.Remove(key);
                }
                else
                {
                    _dictionary[key] = value;
                }
            }
        }

        public long? ContentLength
        {
            get
            {
                var rawValue = this[HeaderNames.ContentLength];
                if (rawValue.Count == 1 &&
                    !string.IsNullOrEmpty(rawValue[0]) &&
                    HeaderUtilities.TryParseNonNegativeInt64(new StringSegment(rawValue[0]).Trim(), out long value))
                {
                    return value;
                }

                return null;
            }
            set
            {
                ThrowIfReadOnly();
                if (value.HasValue)
                {
                    this[HeaderNames.ContentLength] = HeaderUtilities.FormatNonNegativeInt64(value.GetValueOrDefault());
                }
                else
                {
                    this.Remove(HeaderNames.ContentLength);
                }
            }
        }

        public ICollection<string> Keys => _dictionary.Keys;

        public ICollection<StringValues> Values => _dictionary.Values;

        public int Count => _dictionary.Count;

        public bool IsReadOnly { get; set; }

        public void Add(string key, StringValues value)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }
            ThrowIfReadOnly();
            _dictionary.Add(key, value);
        }

        public void Add(KeyValuePair<string, StringValues> item)
        {
            if (item.Key == null)
            {
                throw new ArgumentNullException("The key is null");
            }
            ThrowIfReadOnly();
            _dictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            ThrowIfReadOnly();
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<string, StringValues> item)
        {
            if (!_dictionary.TryGetValue(item.Key, out var value) ||
                !StringValues.Equals(value, item.Value))
            {
                return false;
            }

            return true;
        }

        public bool ContainsKey(string key) => _dictionary.ContainsKey(key);

        public void CopyTo(KeyValuePair<string, StringValues>[] array, int arrayIndex)
        {
            foreach (var item in _dictionary)
            {
                array[arrayIndex] = item;
                arrayIndex++;
            }
        }

        public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator() => _dictionary.GetEnumerator();

        public bool Remove(string key)
        {
            ThrowIfReadOnly();

            return _dictionary.Remove(key);
        }

        public bool Remove(KeyValuePair<string, StringValues> item)
        {
            ThrowIfReadOnly();
            if (_dictionary.TryGetValue(item.Key, out var value) && StringValues.Equals(item.Value, value))
            {
                return _dictionary.Remove(item.Key);
            }

            return false;
        }

        public bool TryGetValue(string key, out StringValues value) => _dictionary.TryGetValue(key, out value);

        IEnumerator IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();

        private void ThrowIfReadOnly()
        {
            if (IsReadOnly)
            {
                throw new InvalidOperationException("The response headers cannot be modified because the response has already started.");
            }
        }
    }
}
