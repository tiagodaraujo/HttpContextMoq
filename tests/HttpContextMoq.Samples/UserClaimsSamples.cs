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
            // Arrange
            var context = new HttpContextMock();

            // Act
            context.UserMock.Mock.Setup(u => u.HasClaim(type, value)).Returns(true);

            // Assert
            context.User.HasClaim(type, value).Should().BeTrue();
            context.User.HasClaim(secondType, value).Should().BeFalse();
        }
    }
}
