using System;
using System.Linq.Expressions;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq.Tests
{
    public class MethodInvokeUnitTest<TContextMock, TContext> : UnitTest<TContextMock>
        where TContext : class
        where TContextMock : class, IContextMock<TContext>, TContext
    {
        private readonly Expression<Action<TContext>> _invokeExpression;
        private readonly Func<Times> _times;

        public MethodInvokeUnitTest(Expression<Action<TContext>> invokeExpression, Func<Times> times = null)
        {
            _invokeExpression = invokeExpression;
            _times = times;
        }

        public override void Run(Func<TContextMock> targetFactory)
        {
            //arrange
            var target = targetFactory.Invoke();

            //act
            _invokeExpression.Compile().Invoke(target);

            //assert
            target.Mock.Verify(_invokeExpression, _times ?? Times.Once);
        }
    }
}
