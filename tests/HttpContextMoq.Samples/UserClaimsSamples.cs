using FluentAssertions;
using Xunit;

namespace HttpContextMoq.Samples
{
    public class UserClaimsSamples
    {
        private const string type = "httpcontextmoq.custom.claim";
        private const string secondType = "httpcontextmoq.custom.secondClaim";
        private const string value = "custom.value";

        [Fact]
        public void User()
        {
            var context = HttpContextMockBuilder.Create().Build();

            context.UserMock.Mock.Setup(u => u.HasClaim(type, value)).Returns(true);

            context.User.HasClaim(type, value).Should().BeTrue();
            context.User.HasClaim(secondType, value).Should().BeFalse();
        }
    }
}
