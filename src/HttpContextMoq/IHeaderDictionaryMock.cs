using HttpContextMoq.Generic;
using Microsoft.AspNetCore.Http;

namespace HttpContextMoq;

public interface IHeaderDictionaryMock : IHeaderDictionary, IContextMock<IHeaderDictionary>
{
}
