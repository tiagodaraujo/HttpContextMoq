using System;

namespace HttpContextMoq.Tests;

public class AssertUnitTest<TTarget>(params Action<TTarget>[] asserts) : UnitTest<TTarget> where TTarget : class
{
    public override void Run(Func<TTarget> targetFactory)
    {
        // Arrange
        var target = targetFactory.Invoke();

        // Assert
        foreach (var assert in asserts)
        {
            assert(target);
        }
    }
}
