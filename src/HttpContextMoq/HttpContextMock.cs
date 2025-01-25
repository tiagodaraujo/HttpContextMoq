﻿using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using HttpContextMoq.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Moq;
#if NETSTANDARD
using Microsoft.AspNetCore.Http.Authentication;
#endif

namespace HttpContextMoq;

public class HttpContextMock : HttpContext, IContextMocks<HttpContext>
{
    private HttpRequest _request;
    private HttpResponse _response;
    private IFeatureCollection _features;
    private ISession _session;
    private ConnectionInfo _connection;

    public HttpContextMock()
    {
        this.Mock = new Mock<HttpContext>();
        this.Mocks = new MockCollection(this);
        this.RequestMock = new HttpRequestMock(this);
        this.ResponseMock = new HttpResponseMock(this);
        this.FeaturesMock = new FeatureCollectionMock();
        this.ConnectionMock = new ConnectionInfoMock();
        this.ItemsMock = new ItemsDictionaryMock();
        this.UserMock = new ClaimsPrincipalMock();
        this.RequestServicesMock = new ServiceProviderMock();
    }

    public Mock<HttpContext> Mock { get; }

    public MockCollection Mocks { get; }

    public HttpRequestMock RequestMock
    {
        get => _request as HttpRequestMock;
        set
        {
            this._request = value;
            this.Mocks.Add(value);
        }
    }

    public HttpResponseMock ResponseMock
    {
        get => _response as HttpResponseMock;
        set
        {
            this._response = value;
            this.Mocks.Add(value);
        }
    }

    public FeatureCollectionMock FeaturesMock
    {
        get => _features as FeatureCollectionMock;
        set
        {
            this._features = value;
            this.Mocks.Add(value);
        }
    }

    public ConnectionInfoMock ConnectionMock
    {
        get => _connection as ConnectionInfoMock;
        set
        {
            this._connection = value;
            this.Mocks.Add(value);
        }
    }

    public IItemsDictionaryMock ItemsMock
    {
        get => this.Items as IItemsDictionaryMock;
        set
        {
            this.Items = value;
            this.Mocks.Add(this.ItemsMock);
        }
    }

    public SessionMock SessionMock
    {
        get => this.Session as SessionMock;
        set
        {
            this.Session = value;
            this.Mocks.Add(this.SessionMock);
        }
    }

    public ClaimsPrincipalMock UserMock
    {
        get => this.User as ClaimsPrincipalMock;
        set
        {
            this.User = value;
            this.Mocks.Add(this.UserMock);
        }
    }

    public ServiceProviderMock RequestServicesMock
    {
        get => this.RequestServices as ServiceProviderMock;
        set
        {
            this.RequestServices = value;
            this.Mocks.Add(this.RequestServicesMock);
        }
    }

    public override ConnectionInfo Connection => _connection;

    public override IFeatureCollection Features => _features;

    public override IDictionary<object, object> Items { get; set; }

    public override HttpRequest Request => _request;

    public override CancellationToken RequestAborted { get => this.Mock.Object.RequestAborted; set => this.Mock.Object.RequestAborted = value; }

    public override IServiceProvider RequestServices { get; set; }

    public override HttpResponse Response => _response;

    public override ISession Session
    {
        get
        {
            if (_session == null)
            {
                throw new InvalidOperationException("Session has not been configured for this application or request.");
            }

            return _session;
        }
        set
        {
            _session = value;
        }
    }

    public override string TraceIdentifier { get => this.Mock.Object.TraceIdentifier; set => this.Mock.Object.TraceIdentifier = value; }

    public override ClaimsPrincipal User { get; set; }

    public override WebSocketManager WebSockets => this.Mock.Object.WebSockets;

    public override void Abort() => this.Mock.Object.Abort();

#if NETSTANDARD
    [Obsolete]
    public override AuthenticationManager Authentication => this.Mock.Object.Authentication;
#endif
}
