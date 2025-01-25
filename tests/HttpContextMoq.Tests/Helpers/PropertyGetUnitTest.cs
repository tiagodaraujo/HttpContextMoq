using System;
using System.Linq.Expressions;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq.Tests;

public class PropertyGetUnitTest<TContextMock, TContext, TProperty>(
    Expression<Func<TContext, TProperty>> getterExpression,
    Func<Times> times = null) : UnitTest<TContextMock>
    where TContext : class
    where TContextMock : class, IContextMock<TContext>, TContext
{
    public override void Run(Func<TContextMock> targetFactory)
    {
        // Arrange
        var target = targetFactory.Invoke();

        // Act
        getterExpression.Compile().Invoke(target);

        // Assert
        target.Mock.VerifyGet(getterExpression, times ?? Times.Once);
    }
}
