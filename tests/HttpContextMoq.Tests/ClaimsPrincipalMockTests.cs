using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Security.Principal;
using FluentAssertions;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests
{
    public class ClaimsPrincipalMockTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void ClaimsPrincipalMock_WhenRun_AssertTrue(UnitTest<ClaimsPrincipalMock> unitTest)
        {
            unitTest.Run(() => new ClaimsPrincipalMock());
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<ClaimsPrincipalMock>[]
            {
                //Class
                new ContextMockUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(),
                //Properties
                new PropertyGetUnitTest<ClaimsPrincipalMock, ClaimsPrincipal, IEnumerable<Claim>>(
                    t => t.Claims
                ),
                new PropertyGetUnitTest<ClaimsPrincipalMock, ClaimsPrincipal, IEnumerable<ClaimsIdentity>>(
                    t => t.Identities
                ),
                new PropertyGetUnitTest<ClaimsPrincipalMock, ClaimsPrincipal, IIdentity>(
                    t => t.Identity, Times.Never
                ),
                new FuncAndAssertResultUnitTest<ClaimsPrincipalMock, IIdentity>(
                    t => t.IdentityMock = new ClaimsIdentityMock(),
                    (t, v) => t.Identity.Should().BeSameAs(v)
                ),
                //Methods
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.AddIdentities(It.IsAny<IEnumerable<ClaimsIdentity>>())
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.AddIdentity(It.IsAny<ClaimsIdentity>())
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.Clone()
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.FindAll(It.IsAny<Predicate<Claim>>())
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.FindAll(It.IsAny<string>())
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.FindFirst(It.IsAny<Predicate<Claim>>())
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.FindFirst(It.IsAny<string>())
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.HasClaim(It.IsAny<Predicate<Claim>>())
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.HasClaim(It.IsAny<string>(), It.IsAny<string>())
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.IsInRole(It.IsAny<string>())
                ),
                new MethodInvokeUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(
                    t => t.WriteTo(It.IsAny<BinaryWriter>())
                ),
            }.ToData();
    }
}
