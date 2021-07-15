namespace HttpContextMoq.Tests
{
    public abstract class UnitTest<TTarget> where TTarget : class
    {
        public abstract void Run(TTarget target);
    }
}
