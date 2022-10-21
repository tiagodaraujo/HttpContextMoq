using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.AspNetCore.Http;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests
{
    public class ConnectionInfoMockTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void ConnectionInfoMock_WhenRun_AssertTrue(UnitTest<ConnectionInfoMock> unitTest)
        {
            unitTest.Run(() => new ConnectionInfoMock());
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<ConnectionInfoMock>[]
            {
                //Class
                new ContextMockUnitTest<ConnectionInfoMock, ConnectionInfo>(),
                //Properties
                new PropertyGetSetUnitTest<ConnectionInfoMock, ConnectionInfo, X509Certificate2>(
                    t => t.ClientCertificate,
                    t => t.ClientCertificate = null
                ),
                new PropertyGetSetUnitTest<ConnectionInfoMock, ConnectionInfo, string>(
                    t => t.Id,
                    t => t.Id = Fakes.String
                ),
                new PropertyGetSetUnitTest<ConnectionInfoMock, ConnectionInfo, IPAddress>(
                    t => t.LocalIpAddress,
                    t => t.LocalIpAddress = IPAddress.Loopback
                ),
                new PropertyGetSetUnitTest<ConnectionInfoMock, ConnectionInfo, int>(
                    t => t.LocalPort,
                    t => t.LocalPort = Fakes.Int
                ),
                new PropertyGetSetUnitTest<ConnectionInfoMock, ConnectionInfo, IPAddress>(
                    t => t.RemoteIpAddress,
                    t => t.RemoteIpAddress = IPAddress.Loopback
                ),
                new PropertyGetSetUnitTest<ConnectionInfoMock, ConnectionInfo, int>(
                    t => t.RemotePort,
                    t => t.RemotePort = Fakes.Int
                ),
                //Methods
                new MethodInvokeUnitTest<ConnectionInfoMock, ConnectionInfo>(
                    t => t.GetClientCertificateAsync(It.IsAny<CancellationToken>())
                ),
            }.ToData();
    }
}
