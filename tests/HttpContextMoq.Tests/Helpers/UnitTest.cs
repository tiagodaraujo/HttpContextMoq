using System;

namespace HttpContextMoq.Tests;

public abstract class UnitTest<TTarget> where TTarget : class
{
    public abstract void Run(Func<TTarget> targetFactory);
}
