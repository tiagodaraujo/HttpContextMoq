namespace HttpContextMoq
{
    using System.Collections.Generic;
    using System.Linq;

    public class MockCollection
    {
        private readonly HashSet<object> collection;

        public MockCollection(object context)
        {
            this.collection = new HashSet<object>();
            this.Add(context);
        }

        public T Get<T>() where T : class
        {
            return collection.FirstOrDefault(i => i is T) as T;
        }

        public void Add<T>(T mock) where T: class
        {
            collection.RemoveWhere(i => i is T);
            collection.Add(mock);
        }
    }
}
