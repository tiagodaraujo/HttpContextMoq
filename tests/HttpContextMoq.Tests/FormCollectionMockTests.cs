using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests;

public class FormCollectionMockTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void FormCollectionMock_WhenRun_AssertTrue(UnitTest<FormCollectionMock> unitTest)
    {
        unitTest.Run(() => new FormCollectionMock());
    }

    public static IEnumerable<object[]> Data =>
        new UnitTest<FormCollectionMock>[]
        {
            //Class
            new ContextMockUnitTest<FormCollectionMock, IFormCollection>(),
            new AssertUnitTest<FormCollectionMock>(
                t => t.FilesMock.Should().NotBeNull(),
                t => t.Mocks.Get<FormFileCollectionMock>().Should().NotBeNull()
            ),
            //Properties
            new FuncAndAssertResultUnitTest<FormCollectionMock, FormFileCollectionMock>(
                t => t.FilesMock = new FormFileCollectionMock(),
                (t, v) => t.FilesMock.Should().BeSameAs(v),
                (t, v) => t.Files.Should().BeSameAs(v),
                (t, v) => t.Mocks.Get<FormFileCollectionMock>().Should().BeSameAs(v)
            ),
            new PropertyGetUnitTest<FormCollectionMock, IFormCollection, StringValues>(
                t => t[Fakes.String]
            ),
            new PropertyGetUnitTest<FormCollectionMock, IFormCollection, int>(
                t => t.Count
            ),
            new PropertyGetUnitTest<FormCollectionMock, IFormCollection, ICollection<string>>(
                t => t.Keys
            ),
            //Methods
            new MethodInvokeUnitTest<FormCollectionMock, IFormCollection>(
                t => t.ContainsKey(It.IsAny<string>())
            ),
            new MethodInvokeUnitTest<FormCollectionMock, IFormCollection>(
                t => t.GetEnumerator()
            ),
            new ActionAndAssertUnitTest<FormCollectionMock>(
                t => ((IEnumerable)t).GetEnumerator(),
                t => t.Mock.As<IEnumerable>().Verify(x => x.GetEnumerator())
            ),
            new MethodInvokeUnitTest<FormCollectionMock, IFormCollection>(
                t => t.TryGetValue(Fakes.String, out Fakes.OutStringValues)
            ),
        }.ToData();
}
