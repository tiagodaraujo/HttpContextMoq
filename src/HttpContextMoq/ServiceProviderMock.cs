namespace HttpContextMoq
{
    using System;
    using Moq;

    public class ServiceProviderMock : IServiceProvider
    {
        public ServiceProviderMock()
        {
            this.Mock = new Mock<IServiceProvider>();
        }

        public Mock<IServiceProvider> Mock { get; }

        public object GetService(Type serviceType) => this.Mock.Object.GetService(serviceType);
    }
}