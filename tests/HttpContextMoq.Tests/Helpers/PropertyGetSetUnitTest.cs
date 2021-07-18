using System;
using System.Linq.Expressions;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq.Tests
{
    public class PropertyGetSetUnitTest<TContextMock, TContext, TProperty> : UnitTest<TContextMock>
        where TContext : class
        where TContextMock : class, IContextMock<TContext>, TContext
    {
        private readonly PropertyGetUnitTest<TContextMock, TContext, TProperty> _getUnitTest;
        private readonly PropertySetUnitTest<TContextMock, TContext> _setUnitTest;

        public PropertyGetSetUnitTest(
            Expression<Func<TContext, TProperty>> getterExpression,
            Action<TContext> setterExpression,
            Func<Times> times = null)
        {
            _getUnitTest = new PropertyGetUnitTest<TContextMock, TContext, TProperty>(getterExpression, times);
            _setUnitTest = new PropertySetUnitTest<TContextMock, TContext>(setterExpression, times);
        }

        public override void Run(Func<TContextMock> targetFactory)
        {
            _getUnitTest.Run(targetFactory);
            _setUnitTest.Run(targetFactory);
        }
    }
}
