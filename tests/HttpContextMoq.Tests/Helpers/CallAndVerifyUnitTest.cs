using System;

namespace HttpContextMoq.Tests
{
    public class CallAndVerifyUnitTest<TTarget> : UnitTest<TTarget> where TTarget: class
    {
        private readonly Action<TTarget> _act;
        private readonly Action<TTarget> _assert;

        public CallAndVerifyUnitTest(Action<TTarget> act, Action<TTarget> assert)
        {
            _act = act;
            _assert = assert;
        }

        public override void Run(TTarget target)
        {
            //Act
            _act(target);

            //Assert
            _assert(target);
        }
    }
}
