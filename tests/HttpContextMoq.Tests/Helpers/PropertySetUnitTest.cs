using System;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq.Tests
{
    public class PropertySetUnitTest<TContextMock, TContext> : UnitTest<TContextMock>
        where TContext : class
        where TContextMock : class, IContextMock<TContext>, TContext
    {
        private readonly Action<TContext> _setterExpression;
        private readonly Func<Times> _times;

        public PropertySetUnitTest(Action<TContext> setterExpression, Func<Times> times = null)
        {
            _setterExpression = setterExpression;
            _times = times;
        }

        public override void Run(Func<TContextMock> targetFactory)
        {
            // Arrange
            var target = targetFactory.Invoke();

            // Act
            _setterExpression.Invoke(target);

            // Assert
            target.Mock.VerifySet(_setterExpression, _times ?? Times.Once);
        }
    }
}
