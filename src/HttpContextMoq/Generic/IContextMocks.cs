namespace HttpContextMoq.Generic
{
    public interface IContextMocks<TMock> : IContextMock<TMock> where TMock: class
    {
        public MockCollection Mocks { get; }
    }
}
