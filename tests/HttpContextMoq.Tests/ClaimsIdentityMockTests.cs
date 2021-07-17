using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using Moq;
using Xunit;

namespace HttpContextMoq.Tests
{
    public class ClaimsIdentityMockTests
    {
        [Theory]
        [MemberData(nameof(Data))]
        public void ClaimsIdentityMock_WhenRun_AssertTrue(UnitTest<ClaimsIdentityMock> unitTest)
        {
            var target = new ClaimsIdentityMock();

            unitTest.Run(target);
        }

        public static IEnumerable<object[]> Data =>
            new UnitTest<ClaimsIdentityMock>[]
            {
                //Class
                new ContextMockUnitTest<ClaimsIdentityMock, ClaimsIdentity>(),
                //Properties
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => _ = t.AuthenticationType,
                    t => t.Mock.VerifyGet(x => x.AuthenticationType, Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => _ = t.Claims,
                    t => t.Mock.VerifyGet(x => x.Claims, Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => _ = t.IsAuthenticated,
                    t => t.Mock.VerifyGet(x => x.IsAuthenticated, Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => _ = t.Name,
                    t => t.Mock.VerifyGet(x => x.Name, Times.Once)
                ),
                //Methods
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.AddClaim(null),
                    t => t.Mock.Verify(x => x.AddClaim(It.IsAny<Claim>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.AddClaim(null),
                    t => t.Mock.Verify(x => x.AddClaim(It.IsAny<Claim>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.AddClaims(null),
                    t => t.Mock.Verify(x => x.AddClaims(It.IsAny<IEnumerable<Claim>>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.Clone(),
                    t => t.Mock.Verify(x => x.Clone(), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.FindAll(c => true),
                    t => t.Mock.Verify(x => x.FindAll(It.IsAny<Predicate<Claim>>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.FindAll(string.Empty),
                    t => t.Mock.Verify(x => x.FindAll(It.IsAny<string>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.FindFirst(c => true),
                    t => t.Mock.Verify(x => x.FindFirst(It.IsAny<Predicate<Claim>>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.FindFirst(string.Empty),
                    t => t.Mock.Verify(x => x.FindFirst(It.IsAny<string>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.HasClaim(c => true),
                    t => t.Mock.Verify(x => x.HasClaim(It.IsAny<Predicate<Claim>>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.HasClaim(null, null),
                    t => t.Mock.Verify(x => x.HasClaim(It.IsAny<string>(), It.IsAny<string>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.TryRemoveClaim(null),
                    t => t.Mock.Verify(x => x.TryRemoveClaim(It.IsAny<Claim>()), Times.Once)
                ),
                new CallAndVerifyUnitTest<ClaimsIdentityMock>(
                    t => t.WriteTo(null),
                    t => t.Mock.Verify(x => x.WriteTo(It.IsAny<BinaryWriter>()), Times.Once)
                ),
            }.ToData();
    }
}
