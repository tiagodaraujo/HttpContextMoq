using System;

namespace HttpContextMoq.Tests
{
    public class AssertUnitTest<TTarget> : UnitTest<TTarget> where TTarget : class
    {
        private readonly Action<TTarget>[] _asserts;

        public AssertUnitTest(params Action<TTarget>[] asserts)
        {
            _asserts = asserts;
        }

        public override void Run(Func<TTarget> targetFactory)
        {
            //arrange
            var target = targetFactory.Invoke();

            //Assert
            foreach (var assert in _asserts)
            {
                assert(target);
            }
        }
    }
}
