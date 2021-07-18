using System;
using FluentAssertions;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq.Tests
{
    public class ContextMockUnitTest<TTarget, TSubTarget> : UnitTest<TTarget>
        where TSubTarget : class
        where TTarget : class, IContextMock<TSubTarget>
    {
        public override void Run(Func<TTarget> targetFactory)
        {
            //Act
            var target = targetFactory.Invoke();

            //Assert
            target.Mock.Should().NotBeNull();
            target.Mock.Should().BeOfType(typeof(Mock<TSubTarget>));

            if (target is IContextMocks<TSubTarget> contextMocks)
            {
                contextMocks.Mocks.Should().NotBeNull();
                contextMocks.Mocks.Should().BeOfType(typeof(MockCollection));
            }
        }
    }
}
