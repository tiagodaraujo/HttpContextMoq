using System;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace HttpContextMoq.Samples
{
    public class RequestServicesSamples
    {
        [Fact]
        public void RequestService()
        {
            var context = HttpContextMockBuilder.Create().Build();

            context.RequestServicesMock.Mock.Setup(x => x.GetService(typeof(RequestServicesSamples))).Returns(new RequestServicesSamples());

            context.RequestServices.GetRequiredService<RequestServicesSamples>().Should().NotBeNull();
            Assert.Throws<InvalidOperationException>(() => context.RequestServices.GetRequiredService<object>());
        }
    }
}
