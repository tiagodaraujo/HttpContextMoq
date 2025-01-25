using System;

namespace HttpContextMoq.Tests;

public class ActionAndAssertUnitTest<TTarget>(
    Action<TTarget> act,
    params Action<TTarget>[] asserts)
    : UnitTest<TTarget> where TTarget : class
{
    public override void Run(Func<TTarget> targetFactory)
    {
        // Arrange
        var target = targetFactory.Invoke();

        // Act
        act(target);

        // Assert
        foreach (var assert in asserts)
        {
            assert(target);
        }
    }
}
