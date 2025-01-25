using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using HttpContextMoq.Generic;
using Moq;

namespace HttpContextMoq;

public class ClaimsIdentityMock : ClaimsIdentity, IContextMock<ClaimsIdentity>
{
    public ClaimsIdentityMock()
    {
        this.Mock = new Mock<ClaimsIdentity>();
    }

    public Mock<ClaimsIdentity> Mock { get; }

    public override string AuthenticationType => this.Mock.Object.AuthenticationType;

    public override IEnumerable<Claim> Claims => this.Mock.Object.Claims;

    public override bool IsAuthenticated => this.Mock.Object.IsAuthenticated;

    public override string Name => this.Mock.Object.Name;

    public override void AddClaim(Claim claim) => this.Mock.Object.AddClaim(claim);

    public override void AddClaims(IEnumerable<Claim> claims) => this.Mock.Object.AddClaims(claims);

    public override ClaimsIdentity Clone() => this.Mock.Object.Clone();

    public override IEnumerable<Claim> FindAll(Predicate<Claim> match) => this.Mock.Object.FindAll(match);

    public override IEnumerable<Claim> FindAll(string type) => this.Mock.Object.FindAll(type);

    public override Claim FindFirst(Predicate<Claim> match) => this.Mock.Object.FindFirst(match);

    public override Claim FindFirst(string type) => this.Mock.Object.FindFirst(type);

    public override bool HasClaim(Predicate<Claim> match) => this.Mock.Object.HasClaim(match);

    public override bool HasClaim(string type, string value) => this.Mock.Object.HasClaim(type, value);

    public override void RemoveClaim(Claim claim) => this.Mock.Object.RemoveClaim(claim);

    public override bool TryRemoveClaim(Claim claim) => this.Mock.Object.TryRemoveClaim(claim);

    public override void WriteTo(BinaryWriter writer) => this.Mock.Object.WriteTo(writer);
}
