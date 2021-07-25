using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Xunit;

namespace HttpContextMoq.Tests
{
    public class RequestCookieCollectionMockTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void RequestCookieCollectionMock_WhenRun_AssertTrue(UnitTest<RequestCookieCollectionMock> unitTest)
        {
            unitTest.Run(() => new RequestCookieCollectionMock());
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<RequestCookieCollectionMock>[]
            {
                //Class
                new ContextMockUnitTest<RequestCookieCollectionMock, IRequestCookieCollection>(),
                //Properties
                new PropertyGetUnitTest<RequestCookieCollectionMock, IRequestCookieCollection, string>(
                    t => t[Fakes.String]
                ),
                new PropertyGetUnitTest<RequestCookieCollectionMock, IRequestCookieCollection, int>(
                    t => t.Count
                ),
                new PropertyGetUnitTest<RequestCookieCollectionMock, IRequestCookieCollection, ICollection<string>>(
                    t => t.Keys
                ),
                //Methods
                new MethodInvokeUnitTest<RequestCookieCollectionMock, IRequestCookieCollection>(
                    t => t.ContainsKey(Fakes.String)
                ),
                new MethodInvokeUnitTest<RequestCookieCollectionMock, IRequestCookieCollection>(
                    t => t.GetEnumerator()
                ),
                new ActionAndAssertUnitTest<RequestCookieCollectionMock>(
                    t => ((IEnumerable)t).GetEnumerator(),
                    t => t.Mock.As<IEnumerable>().Verify(x => x.GetEnumerator())
                ),
                new MethodInvokeUnitTest<RequestCookieCollectionMock, IRequestCookieCollection>(
                    t => t.TryGetValue(Fakes.String, out Fakes.OutString)
                ),
            }.ToData();
    }
}
