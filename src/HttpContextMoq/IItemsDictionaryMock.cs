using System.Collections.Generic;
using HttpContextMoq.Generic;

namespace HttpContextMoq
{
    public interface IItemsDictionaryMock : IDictionary<object, object>, IContextMock<IDictionary<object, object>>
    {
    }
}
