using System;
using System.Linq.Expressions;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq.Tests;

public class PropertyGetSetUnitTest<TContextMock, TContext, TProperty>(
    Expression<Func<TContext, TProperty>> getterExpression,
    Action<TContext> setterExpression,
    Func<Times> times = null)
    : UnitTest<TContextMock>
    where TContext : class
    where TContextMock : class, IContextMock<TContext>, TContext
{
    private readonly PropertyGetUnitTest<TContextMock, TContext, TProperty> _getUnitTest = new(getterExpression, times);
    private readonly PropertySetUnitTest<TContextMock, TContext> _setUnitTest = new(setterExpression, times);

    public override void Run(Func<TContextMock> targetFactory)
    {
        _getUnitTest.Run(targetFactory);
        _setUnitTest.Run(targetFactory);
    }
}
