using System;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq.Tests;

public class PropertySetUnitTest<TContextMock, TContext>(
    Action<TContext> setterExpression,
    Func<Times> times = null)
    : UnitTest<TContextMock>
    where TContext : class
    where TContextMock : class, IContextMock<TContext>, TContext
{
    public override void Run(Func<TContextMock> targetFactory)
    {
        // Arrange
        var target = targetFactory.Invoke();

        // Act
        setterExpression.Invoke(target);

        // Assert
        target.Mock.VerifySet(setterExpression, times ?? Times.Once);
    }
}
