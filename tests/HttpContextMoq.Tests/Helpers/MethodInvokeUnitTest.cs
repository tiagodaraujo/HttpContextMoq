using System;
using System.Linq.Expressions;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq.Tests;

public class MethodInvokeUnitTest<TContextMock, TContext>(
    Expression<Action<TContext>> invokeExpression,
    Func<Times> times = null) : UnitTest<TContextMock>
    where TContext : class
    where TContextMock : class, IContextMock<TContext>, TContext
{
    public override void Run(Func<TContextMock> targetFactory)
    {
        // Arrange
        var target = targetFactory.Invoke();

        // Act
        invokeExpression.Compile().Invoke(target);

        // Assert
        target.Mock.Verify(invokeExpression, times ?? Times.Once);
    }
}
