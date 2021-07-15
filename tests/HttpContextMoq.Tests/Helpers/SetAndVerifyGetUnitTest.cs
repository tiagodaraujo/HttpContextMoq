using System;

namespace HttpContextMoq.Tests
{
    public class SetAndVerifyGetUnitTest<TTarget, TResult> : UnitTest<TTarget>
        where TTarget : class
        where TResult : class
    {
        private readonly Func<TTarget, TResult> _act;
        private readonly Action<TTarget, TResult> _assert;

        public SetAndVerifyGetUnitTest(Func<TTarget, TResult> act, Action<TTarget, TResult> assert) 
        {
            _act = act;
            _assert = assert;
        }

        public override void Run(TTarget target)
        {
            //Act
            var result = _act(target);

            //Assert
            _assert(target, result);
        }
    }
}
