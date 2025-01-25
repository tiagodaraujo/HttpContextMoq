using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace HttpContextMoq.Tests;

public class HttpContextAccessorMockTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void HttpContextAccessorMock_WhenRun_AssertTrue(UnitTest<HttpContextAccessorMock> unitTest)
    {
        unitTest.Run(() => new HttpContextAccessorMock());
    }

    public static IEnumerable<object[]> Data =>
        new UnitTest<HttpContextAccessorMock>[]
        {
            //Class
            new AssertUnitTest<HttpContextAccessorMock>(
                t => t.HttpContextMock.Should().NotBeNull(),
                t => t.HttpContext.Should().NotBeNull()
            ),
            //Properties
            new FuncAndAssertResultUnitTest<HttpContextAccessorMock, HttpContextMock>(
                t => t.HttpContextMock = new HttpContextMock(),
                (t, v) => t.HttpContextMock.Should().BeSameAs(v),
                (t, v) => t.HttpContext.Should().BeSameAs(v)
            ),
            new FuncAndAssertResultUnitTest<HttpContextAccessorMock, HttpContext>(
                t => t.HttpContext = new DefaultHttpContext(),
                (t, v) => t.HttpContextMock.Should().BeNull(),
                (t, v) => t.HttpContext.Should().BeSameAs(v)
            ),
        }.ToData();
}
