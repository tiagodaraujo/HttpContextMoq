using System.Text;
using FluentAssertions;
using HttpContextMoq.Extensions;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace HttpContextMoq.Samples;

public class SessionSamples
{
    [Fact]
    public void SetupSession_WhenIfCalledWithMock_ShouldReturnValue()
    {
        ///
        /// Default setup using the SessionMock. You can mock any Method or Property from the ISession interface.
        /// In this sample, TryGetValue is mocked to return a byte[].
        ///

        // Arrange
        var context = new HttpContextMock();
        context.SetupSession();

        var value = Encoding.UTF8.GetBytes("Mike");
        context.SessionMock.Mock.Setup(x => x.TryGetValue("Name", out value)).Returns(true);

        // Act
        var result = context.Session.GetString("Name");

        // Assert
        result.Should().Be("Mike");
    }

    [Fact]
    public void SetupSession_IfCalledWithFake_ShouldReturnValue()
    {
        ///
        /// Use the SessionFake to mock your session with a Fake implementation. Every method or extension for ISession works.
        /// You can also create your own ISession implementaion and use it in the SetupSession<T>(). e.g. context.SetupSession<MyOwnSessionFake>().
        ///

        // Arrange
        var context = new HttpContextMock();
        context.SetupSession<SessionFake>();

        context.Session.SetString("Name", "Mike");

        // Act
        var result = context.Session.GetString("Name");

        // Assert
        result.Should().Be("Mike");
    }
}
