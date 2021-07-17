using FluentAssertions;
using HttpContextMoq.Generic;
using Xunit;

namespace HttpContextMoq.Tests.Generic
{
    public class MockCollectionTests
    {
        [Fact]
        public void MockCollection_WhenConstructer_ShouldAddSelfToTheCollection()
        {
            //arrange
            var constructor = new object();

            //act
            var target = new MockCollection(constructor);

            //assert
            var result = target.Get<object>();
            result.Should().BeSameAs(constructor);
        }

        [Fact]
        public void MockCollection_WhenAdd_ExpectGet()
        {
            //arrange
            var expected = new object();
            var constructor = new object();
            var target = new MockCollection(constructor);

            //act
            target.Add(expected);

            //assert
            var result = target.Get<object>();
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void MockCollection_WhenAddTwoOfSameType_ShoudlReplaceTheFirstOne()
        {
            //arrange
            var expected = new object();
            var first = new object();
            var constructor = new object();
            var target = new MockCollection(constructor);

            //act
            target.Add(first);
            target.Add(expected);

            //assert
            var result = target.Get<object>();
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void MockCollection_WhenGetTypeNotExists_ShouldBeNull()
        {
            //arrange
            var constructor = new object();
            var target = new MockCollection(constructor);

            //act
            var result = target.Get<object[]>();

            //assert
            result.Should().BeNull();
        }
    }
}
