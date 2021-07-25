using System.Collections;
using System.Collections.Generic;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq
{
    public class ItemsDictionaryMock : IDictionary<object, object>, IContextMock<IDictionary<object, object>>
    {
        public ItemsDictionaryMock()
        {
            this.Mock = new Mock<IDictionary<object, object>>();
        }

        public Mock<IDictionary<object, object>> Mock { get; }

        object IDictionary<object, object>.this[object key]
        {
            get => this.Mock.Object[key];
            set => this.Mock.Object[key] = value;
        }

        void IDictionary<object, object>.Add(object key, object value) => this.Mock.Object.Add(key, value);

        bool IDictionary<object, object>.ContainsKey(object key) => this.Mock.Object.ContainsKey(key);

        ICollection<object> IDictionary<object, object>.Keys => this.Mock.Object.Keys;

        bool IDictionary<object, object>.Remove(object key) => this.Mock.Object.Remove(key);

        bool IDictionary<object, object>.TryGetValue(object key, out object value) => this.Mock.Object.TryGetValue(key, out value);

        ICollection<object> IDictionary<object, object>.Values => this.Mock.Object.Values;

        void ICollection<KeyValuePair<object, object>>.Add(KeyValuePair<object, object> item) => this.Mock.Object.Add(item);

        void ICollection<KeyValuePair<object, object>>.Clear() => this.Mock.Object.Clear();

        bool ICollection<KeyValuePair<object, object>>.Contains(KeyValuePair<object, object> item) => this.Mock.Object.Contains(item);

        void ICollection<KeyValuePair<object, object>>.CopyTo(KeyValuePair<object, object>[] array, int arrayIndex) => this.Mock.Object.CopyTo(array, arrayIndex);

        int ICollection<KeyValuePair<object, object>>.Count => this.Mock.Object.Count;

        bool ICollection<KeyValuePair<object, object>>.IsReadOnly => this.Mock.Object.IsReadOnly;

        bool ICollection<KeyValuePair<object, object>>.Remove(KeyValuePair<object, object> item) => ((ICollection<KeyValuePair<object, object>>)this.Mock.Object).Remove(item);

        IEnumerator<KeyValuePair<object, object>> IEnumerable<KeyValuePair<object, object>>.GetEnumerator() => this.Mock.Object.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => ((IEnumerable)this.Mock.Object).GetEnumerator();
    }
}
