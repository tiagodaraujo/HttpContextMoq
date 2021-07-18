using System;

namespace HttpContextMoq.Tests
{
    public class FuncAndAssertResultUnitTest<TTarget, TResult> : UnitTest<TTarget>
        where TTarget : class
        where TResult : class
    {
        private readonly Func<TTarget, TResult> _act;
        private readonly Action<TTarget, TResult> _assert;

        public FuncAndAssertResultUnitTest(Func<TTarget, TResult> act, Action<TTarget, TResult> assert) 
        {
            _act = act;
            _assert = assert;
        }

        public override void Run(Func<TTarget> targetFactory)
        {
            //Arrange
            var target = targetFactory.Invoke();

            //Act
            var result = _act(target);

            //Assert
            _assert(target, result);
        }
    }
}
