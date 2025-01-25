using System.Collections.Generic;
using System.Linq;

namespace HttpContextMoq.Tests;

public static class UnitTestExtensions
{
    public static IEnumerable<object[]> ToData<TTarget>(this UnitTest<TTarget>[] unitTests) where TTarget : class =>
        unitTests.Select(u => new object[] { u });
}
