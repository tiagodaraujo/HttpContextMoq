using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace HttpContextMoq.Tests;

public class SessionMockTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void SessionMock_WhenRun_AssertTrue(UnitTest<SessionMock> unitTest)
    {
        unitTest.Run(() => new SessionMock());
    }

    public static IEnumerable<object[]> Data =>
        new UnitTest<SessionMock>[]
        {
            //Class
            new ContextMockUnitTest<SessionMock, ISession>(),
            //Properties
            new PropertyGetUnitTest<SessionMock, ISession, string>(
                t => t.Id
            ),
            new PropertyGetUnitTest<SessionMock, ISession, bool>(
                t => t.IsAvailable
            ),
            new PropertyGetUnitTest<SessionMock, ISession, IEnumerable<string>>(
                t => t.Keys
            ),
            //Methods
            new MethodInvokeUnitTest<SessionMock, ISession>(
                t => t.Clear()
            ),
            new MethodInvokeUnitTest<SessionMock, ISession>(
                t => t.CommitAsync(Fakes.CancellationToken)
            ),
            new MethodInvokeUnitTest<SessionMock, ISession>(
                t => t.LoadAsync(Fakes.CancellationToken)
            ),
            new MethodInvokeUnitTest<SessionMock, ISession>(
                t => t.Remove(Fakes.String)
            ),
            new MethodInvokeUnitTest<SessionMock, ISession>(
                t => t.Set(Fakes.String, Fakes.ByteArray)
            ),
            new MethodInvokeUnitTest<SessionMock, ISession>(
                t => t.TryGetValue(Fakes.String, out Fakes.OutByteArray)
            ),
        }.ToData();
}
