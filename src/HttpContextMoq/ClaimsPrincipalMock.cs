namespace HttpContextMoq
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Security.Claims;
    using System.Security.Principal;
    using Moq;

    public class ClaimsPrincipalMock : ClaimsPrincipal, IContextMock
    {
        private IIdentity _identity;

        public ClaimsPrincipalMock()
        {
            this.Mock = new Mock<ClaimsPrincipal>();
            this.Mocks = new MockCollection(this);
            this.IdentityMock = new ClaimsIdentityMock();
        }

        public Mock<ClaimsPrincipal> Mock { get; }

        public MockCollection Mocks { get; }

        public ClaimsIdentityMock IdentityMock
        {
            get => _identity as ClaimsIdentityMock;
            set
            {
                _identity = value;
                this.Mocks.Add(value);
            }
        }

        public override IEnumerable<Claim> Claims => this.Mock.Object.Claims;

        public override IEnumerable<ClaimsIdentity> Identities => this.Mock.Object.Identities;

        public override IIdentity Identity => _identity;

        public override void AddIdentities(IEnumerable<ClaimsIdentity> identities) => this.Mock.Object.AddIdentities(identities);

        public override void AddIdentity(ClaimsIdentity identity) => this.Mock.Object.AddIdentity(identity);

        public override ClaimsPrincipal Clone() => this.Mock.Object.Clone();

        public override IEnumerable<Claim> FindAll(Predicate<Claim> match) => this.Mock.Object.FindAll(match);

        public override IEnumerable<Claim> FindAll(string type) => this.Mock.Object.FindAll(type);

        public override Claim FindFirst(Predicate<Claim> match) => this.Mock.Object.FindFirst(match);

        public override Claim FindFirst(string type) => this.Mock.Object.FindFirst(type);

        public override bool HasClaim(Predicate<Claim> match) => this.Mock.Object.HasClaim(match);

        public override bool HasClaim(string type, string value) => this.Mock.Object.HasClaim(type, value);

        public override bool IsInRole(string role) => this.Mock.Object.IsInRole(role);

        public override void WriteTo(BinaryWriter writer) => this.Mock.Object.WriteTo(writer);
    }
}
