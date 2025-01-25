using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using HttpContextMoq.Generic;
using Microsoft.AspNetCore.Http;
using Moq;

namespace HttpContextMoq;

public class ConnectionInfoMock : ConnectionInfo, IContextMock<ConnectionInfo>
{
    public ConnectionInfoMock()
    {
        this.Mock = new Mock<ConnectionInfo>();
    }

    public Mock<ConnectionInfo> Mock { get; }

    public override X509Certificate2 ClientCertificate
    {
        get => this.Mock.Object.ClientCertificate;
        set => this.Mock.Object.ClientCertificate = value;
    }

    public override string Id
    {
        get => this.Mock.Object.Id;
        set => this.Mock.Object.Id = value;
    }

    public override IPAddress LocalIpAddress
    {
        get => this.Mock.Object.LocalIpAddress;
        set => this.Mock.Object.LocalIpAddress = value;
    }

    public override int LocalPort
    {
        get => this.Mock.Object.LocalPort;
        set => this.Mock.Object.LocalPort = value;
    }

    public override IPAddress RemoteIpAddress
    {
        get => this.Mock.Object.RemoteIpAddress;
        set => this.Mock.Object.RemoteIpAddress = value;
    }

    public override int RemotePort
    {
        get => this.Mock.Object.RemotePort;
        set => this.Mock.Object.RemotePort = value;
    }

    public override Task<X509Certificate2> GetClientCertificateAsync(CancellationToken cancellationToken = default) => this.Mock.Object.GetClientCertificateAsync(cancellationToken);
}