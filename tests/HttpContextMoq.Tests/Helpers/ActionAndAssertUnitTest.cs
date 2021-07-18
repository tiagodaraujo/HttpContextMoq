using System;

namespace HttpContextMoq.Tests
{
    public class ActionAndAssertUnitTest<TTarget> : UnitTest<TTarget> where TTarget: class
    {
        private readonly Action<TTarget> _act;
        private readonly Action<TTarget> _assert;

        public ActionAndAssertUnitTest(Action<TTarget> act, Action<TTarget> assert)
        {
            _act = act;
            _assert = assert;
        }

        public override void Run(Func<TTarget> targetFactory)
        {
            //arrange
            var target = targetFactory.Invoke();

            //Act
            _act(target);

            //Assert
            _assert(target);
        }
    }
}
