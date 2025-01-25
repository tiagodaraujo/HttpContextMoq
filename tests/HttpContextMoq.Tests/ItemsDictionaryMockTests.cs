using System.Collections;
using System.Collections.Generic;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests;

public class ItemsDictionaryMockTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void ItemsDictionaryMock_WhenRun_AssertTrue(UnitTest<ItemsDictionaryMock> unitTest)
    {
        unitTest.Run(() => new ItemsDictionaryMock());
    }

    public static IEnumerable<object[]> Data =>
        new UnitTest<ItemsDictionaryMock>[]
        {
            //Class
            new ContextMockUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(),
            //Properties
            new PropertyGetSetUnitTest<ItemsDictionaryMock, IDictionary<object, object>, object>(
                t => t[Fakes.Object],
                t => t[Fakes.Object] = Fakes.Object
            ),
            new PropertyGetUnitTest<ItemsDictionaryMock, IDictionary<object, object>, ICollection<object>>(
                t => t.Keys
            ),
            new PropertyGetUnitTest<ItemsDictionaryMock, IDictionary<object, object>, ICollection<object>>(
                t => t.Values
            ),
            new PropertyGetUnitTest<ItemsDictionaryMock, IDictionary<object, object>, int>(
                t => t.Count
            ),
            new PropertyGetUnitTest<ItemsDictionaryMock, IDictionary<object, object>, bool>(
                t => t.IsReadOnly
            ),
            //Methods
            new MethodInvokeUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(
                t => t.Add(Fakes.Object, Fakes.Object)
            ),
            new MethodInvokeUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(
                t => t.ContainsKey(Fakes.Object)
            ),
            new MethodInvokeUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(
                t => t.Remove(Fakes.Object)
            ),
            new MethodInvokeUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(
                t => t.TryGetValue(Fakes.Object, out Fakes.OutObject)
            ),
            new MethodInvokeUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(
                t => t.Add(It.IsAny<KeyValuePair<object, object>>())
            ),
            new MethodInvokeUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(
                t => t.Clear()
            ),
            new MethodInvokeUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(
                t => t.Contains(It.IsAny<KeyValuePair<object, object>>())
            ),
            new MethodInvokeUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(
                t => t.CopyTo(It.IsAny<KeyValuePair<object, object>[]>(), Fakes.Int)
            ),
            new ActionAndAssertUnitTest<ItemsDictionaryMock>(
                t => ((ICollection<KeyValuePair<object, object>>)t).Remove(It.IsAny<KeyValuePair<object, object>>()),
                t => t.Mock.As<ICollection<KeyValuePair<object, object>>>().Verify(x => x.Remove(It.IsAny<KeyValuePair<object, object>>()))
            ),
            new MethodInvokeUnitTest<ItemsDictionaryMock, IDictionary<object, object>>(
                t => t.GetEnumerator()
            ),
            new ActionAndAssertUnitTest<ItemsDictionaryMock>(
                t => ((IEnumerable)t).GetEnumerator(),
                t => t.Mock.As<IEnumerable>().Verify(x => x.GetEnumerator())
            ),
            new ActionAndAssertUnitTest<ItemsDictionaryMock>(
                t => ((IEnumerable)t).GetEnumerator(),
                t => t.Mock.As<IEnumerable>().Verify(x => x.GetEnumerator())
            ),
        }.ToData();
}
