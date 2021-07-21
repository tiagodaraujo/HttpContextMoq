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
            // Arrange
            var constructor = new object();

            // Act
            var target = new MockCollection(constructor);

            // Assert
            var result = target.Get<object>();
            result.Should().BeSameAs(constructor);
        }

        [Fact]
        public void MockCollection_WhenAdd_ExpectGet()
        {
            // Arrange
            var expected = new object();
            var constructor = new object();
            var target = new MockCollection(constructor);

            // Act
            target.Add(expected);

            // Assert
            var result = target.Get<object>();
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void MockCollection_WhenAddTwoOfSameType_ShoudlReplaceTheFirstOne()
        {
            // Arrange
            var expected = new object();
            var first = new object();
            var constructor = new object();
            var target = new MockCollection(constructor);

            // Act
            target.Add(first);
            target.Add(expected);

            // Assert
            var result = target.Get<object>();
            result.Should().BeSameAs(expected);
        }

        [Fact]
        public void MockCollection_WhenGetTypeNotExists_ShouldBeNull()
        {
            // Arrange
            var constructor = new object();
            var target = new MockCollection(constructor);

            // Act
            var result = target.Get<object[]>();

            // Assert
            result.Should().BeNull();
        }
    }
}
