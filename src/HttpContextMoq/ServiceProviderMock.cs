﻿using System;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq;

public class ServiceProviderMock : IServiceProvider, IContextMock<IServiceProvider>
{
    public ServiceProviderMock()
    {
        this.Mock = new Mock<IServiceProvider>();
    }

    public Mock<IServiceProvider> Mock { get; }

    public object GetService(Type serviceType) => this.Mock.Object.GetService(serviceType);
}