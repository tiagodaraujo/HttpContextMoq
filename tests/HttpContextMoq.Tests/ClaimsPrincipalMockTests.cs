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
        public void ClaimsIdentityMock_WhenRun_AssertTrue(UnitTest<ClaimsPrincipalMock> unitTest)
        {
            var target = new ClaimsPrincipalMock();

            unitTest.Run(target);
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<ClaimsPrincipalMock>[]
            {
                //Class
                new ContextMockUnitTest<ClaimsPrincipalMock, ClaimsPrincipal>(),
                //Properties
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => _ = t.Claims,
                    t => t.Mock.VerifyGet(x => x.Claims, Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => _ = t.Identities,
                    t => t.Mock.VerifyGet(x => x.Identities, Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => _ = t.Identity,
                    t => t.Mock.VerifyGet(x => x.Identity, Times.Never)
                ),
                new SetAndVerifyGetUnitTest<ClaimsPrincipalMock, IIdentity>(
                    t => t.IdentityMock = new ClaimsIdentityMock(),
                    (t, v) => t.Identity.Should().BeSameAs(v)
                ),
                //Methods
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.AddIdentities(null),
                    t => t.Mock.Verify(x => x.AddIdentities(It.IsAny<IEnumerable<ClaimsIdentity>>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.AddIdentity(null),
                    t => t.Mock.Verify(x => x.AddIdentity(It.IsAny<ClaimsIdentity>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.Clone(),
                    t => t.Mock.Verify(x => x.Clone(), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.FindAll(c => true),
                    t => t.Mock.Verify(x => x.FindAll(It.IsAny<Predicate<Claim>>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.FindAll(string.Empty),
                    t => t.Mock.Verify(x => x.FindAll(It.IsAny<string>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.FindFirst(c => true),
                    t => t.Mock.Verify(x => x.FindFirst(It.IsAny<Predicate<Claim>>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.FindFirst(string.Empty),
                    t => t.Mock.Verify(x => x.FindFirst(It.IsAny<string>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.HasClaim(c => true),
                    t => t.Mock.Verify(x => x.HasClaim(It.IsAny<Predicate<Claim>>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.HasClaim(null, null),
                    t => t.Mock.Verify(x => x.HasClaim(It.IsAny<string>(), It.IsAny<string>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.IsInRole(null),
                    t => t.Mock.Verify(x => x.IsInRole(It.IsAny<string>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsPrincipalMock>(
                    t => t.WriteTo(null),
                    t => t.Mock.Verify(x => x.WriteTo(It.IsAny<BinaryWriter>()), Times.Once)
                ),
            }.ToData();
    }
}
