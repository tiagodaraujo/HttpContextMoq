using System;

namespace HttpContextMoq.Tests;

public class FuncAndAssertResultUnitTest<TTarget, TResult>(
    Func<TTarget, TResult> act, 
    params Action<TTarget, TResult>[] assert) : UnitTest<TTarget>
    where TTarget : class
    where TResult : class
{
    public FuncAndAssertResultUnitTest(Func<TTarget, TResult> act, Action<TTarget, TResult> assert)
        : this(act, [assert])
    {
    }

    public override void Run(Func<TTarget> targetFactory)
    {
        // Arrange
        var target = targetFactory.Invoke();

        // Act
        var result = act(target);

        // Assert
        foreach (var assert in assert)
        {
            assert(target, result);
        }
    }
}
