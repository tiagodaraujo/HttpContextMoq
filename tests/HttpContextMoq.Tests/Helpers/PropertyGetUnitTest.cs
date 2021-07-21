using System;
using System.Linq.Expressions;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq.Tests
{
    public class PropertyGetUnitTest<TContextMock, TContext, TProperty> : UnitTest<TContextMock>
        where TContext : class
        where TContextMock : class, IContextMock<TContext>, TContext
    {
        private readonly Expression<Func<TContext, TProperty>> _getterExpression;
        private readonly Func<Times> _times;

        public PropertyGetUnitTest(Expression<Func<TContext, TProperty>> getterExpression, Func<Times> times = null)
        {
            _getterExpression = getterExpression;
            _times = times;
        }

        public override void Run(Func<TContextMock> targetFactory)
        {
            // Arrange
            var target = targetFactory.Invoke();

            // Act
            _getterExpression.Compile().Invoke(target);

            // Assert
            target.Mock.VerifyGet(_getterExpression, _times ?? Times.Once);
        }
    }
}
