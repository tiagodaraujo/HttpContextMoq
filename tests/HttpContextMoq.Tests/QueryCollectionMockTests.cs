using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace HttpContextMoq.Tests
{
    public class QueryCollectionMockTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void QueryCollectionMock_WhenRun_AssertTrue(UnitTest<QueryCollectionMock> unitTest)
        {
            unitTest.Run(() => new QueryCollectionMock());
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<QueryCollectionMock>[]
            {
                //Class
                new ContextMockUnitTest<QueryCollectionMock, IQueryCollection>(),
                //Properties
                new PropertyGetUnitTest<QueryCollectionMock, IQueryCollection, StringValues>(
                    t => t[Fakes.String]
                ),
                new PropertyGetUnitTest<QueryCollectionMock, IQueryCollection, int>(
                    t => t.Count
                ),
                new PropertyGetUnitTest<QueryCollectionMock, IQueryCollection, ICollection<string>>(
                    t => t.Keys
                ),
                //Methods
                new MethodInvokeUnitTest<QueryCollectionMock, IQueryCollection>(
                    t => t.ContainsKey(Fakes.String)
                ),
                new MethodInvokeUnitTest<QueryCollectionMock, IQueryCollection>(
                    t => t.GetEnumerator()
                ),
                new ActionAndAssertUnitTest<QueryCollectionMock>(
                    t => ((IEnumerable)t).GetEnumerator(),
                    t => t.Mock.As<IEnumerable>().Verify(x => x.GetEnumerator())
                ),
                new MethodInvokeUnitTest<QueryCollectionMock, IQueryCollection>(
                    t => t.TryGetValue(Fakes.String, out Fakes.OutStringValues)
                ),
            }.ToData();
    }
}
