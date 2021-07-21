using System;

namespace HttpContextMoq.Tests
{
    public class FuncAndAssertResultUnitTest<TTarget, TResult> : UnitTest<TTarget>
        where TTarget : class
        where TResult : class
    {
        private readonly Func<TTarget, TResult> _act;
        private readonly Action<TTarget, TResult>[] _asserts;

        public FuncAndAssertResultUnitTest(Func<TTarget, TResult> act, Action<TTarget, TResult> assert)
            : this(act, new[] { assert })
        {
        }

        public FuncAndAssertResultUnitTest(Func<TTarget, TResult> act, params Action<TTarget, TResult>[] assert)
        {
            _act = act;
            _asserts = assert;
        }

        public override void Run(Func<TTarget> targetFactory)
        {
            // Arrange
            var target = targetFactory.Invoke();

            // Act
            var result = _act(target);

            // Assert
            foreach (var assert in _asserts)
            {
                assert(target, result);
            }
        }
    }
}
