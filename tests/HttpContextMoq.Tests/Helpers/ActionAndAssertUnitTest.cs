using System;

namespace HttpContextMoq.Tests
{
    public class ActionAndAssertUnitTest<TTarget> : UnitTest<TTarget> where TTarget : class
    {
        private readonly Action<TTarget> _act;
        private readonly Action<TTarget>[] _asserts;

        public ActionAndAssertUnitTest(Action<TTarget> act, params Action<TTarget>[] asserts)
        {
            _act = act;
            _asserts = asserts;
        }

        public override void Run(Func<TTarget> targetFactory)
        {
            // Arrange
            var target = targetFactory.Invoke();

            // Act
            _act(target);

            // Assert
            foreach (var assert in _asserts)
            {
                assert(target);
            }
        }
    }
}
