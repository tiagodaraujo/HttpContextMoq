using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests;

public class ClaimsIdentityMockTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public void ClaimsIdentityMock_WhenRun_AssertTrue(UnitTest<ClaimsIdentityMock> unitTest)
    {
        unitTest.Run(() => new ClaimsIdentityMock());
    }

    public static IEnumerable<object[]> Data =>
        new UnitTest<ClaimsIdentityMock>[]
        {
            //Class
            new ContextMockUnitTest<ClaimsIdentityMock, ClaimsIdentity>(),
            //Properties
            new PropertyGetUnitTest<ClaimsIdentityMock, ClaimsIdentity, string>(
                t => t.AuthenticationType
            ),
            new PropertyGetUnitTest<ClaimsIdentityMock, ClaimsIdentity, IEnumerable<Claim>>(
                t => t.Claims
            ),
            new PropertyGetUnitTest<ClaimsIdentityMock, ClaimsIdentity, bool>(
                t => t.IsAuthenticated
            ),
            new PropertyGetUnitTest<ClaimsIdentityMock, ClaimsIdentity, string>(
                t => t.Name
            ),
            //Methods
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.AddClaim(It.IsAny<Claim>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.AddClaim(It.IsAny<Claim>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.AddClaims(It.IsAny<IEnumerable<Claim>>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.Clone()
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.FindAll(It.IsAny<Predicate<Claim>>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.FindAll(It.IsAny<string>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.FindFirst(It.IsAny<Predicate<Claim>>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.FindFirst(It.IsAny<string>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.HasClaim(It.IsAny<Predicate<Claim>>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.HasClaim(It.IsAny<string>(), It.IsAny<string>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.RemoveClaim(It.IsAny<Claim>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.TryRemoveClaim(It.IsAny<Claim>())
            ),
            new MethodInvokeUnitTest<ClaimsIdentityMock, ClaimsIdentity>(
                t => t.WriteTo(It.IsAny<BinaryWriter>())
            ),
        }.ToData();
}
