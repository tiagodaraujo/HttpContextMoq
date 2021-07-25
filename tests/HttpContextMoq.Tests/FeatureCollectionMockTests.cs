using System.Collections;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests
{
    public class FeatureCollectionMockTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void FeatureCollectionMock_WhenRun_AssertTrue(UnitTest<FeatureCollectionMock> unitTest)
        {
            unitTest.Run(() => new FeatureCollectionMock());
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<FeatureCollectionMock>[]
            {
                //Class
                new ContextMockUnitTest<FeatureCollectionMock, IFeatureCollection>(),
                //Properties
                new PropertyGetSetUnitTest<FeatureCollectionMock, IFeatureCollection, object>(
                    t => t[typeof(string)],
                    t => t[typeof(string)] = Fakes.String
                ),
                new PropertyGetUnitTest<FeatureCollectionMock, IFeatureCollection, bool>(
                    t => t.IsReadOnly
                ),
                new PropertyGetUnitTest<FeatureCollectionMock, IFeatureCollection, int>(
                    t => t.Revision
                ),
                //Methods
                new MethodInvokeUnitTest<FeatureCollectionMock, IFeatureCollection>(
                    t => t.Get<FeatureCollectionMockTests>()
                ),
                new MethodInvokeUnitTest<FeatureCollectionMock, IFeatureCollection>(
                    t => t.GetEnumerator()
                ),
                new ActionAndAssertUnitTest<FeatureCollectionMock>(
                    t => ((IEnumerable)t).GetEnumerator(),
                    t => t.Mock.As<IEnumerable>().Verify(x => x.GetEnumerator())
                ),
                new MethodInvokeUnitTest<FeatureCollectionMock, IFeatureCollection>(
                    t => t.Set<FeatureCollectionMockTests>(It.IsAny<FeatureCollectionMockTests>()), Times.Never
                ),
                new ActionAndAssertUnitTest<FeatureCollectionMock>(
                    t => t.Set<string>(Fakes.String),
                    t => t.Get<string>().Should().Be(Fakes.String)
                )
            }.ToData();
    }
}
