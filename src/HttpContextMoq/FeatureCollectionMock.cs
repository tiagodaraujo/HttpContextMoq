namespace HttpContextMoq
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using HttpContextMoq.Generic;
    using Microsoft.AspNetCore.Http.Features;
    using Moq;

    public class FeatureCollectionMock : IFeatureCollection, IContextMock<IFeatureCollection>
    {
        public FeatureCollectionMock()
        {
            this.Mock = new Mock<IFeatureCollection>();
        }

        public Mock<IFeatureCollection> Mock { get; }

        public object this[Type key]
        {
            get => this.Mock.Object[key];
            set => this.Mock.Object[key] = value;
        }

        public bool IsReadOnly => this.Mock.Object.IsReadOnly;

        public int Revision => this.Mock.Object.Revision;

        public TFeature Get<TFeature>() => this.Mock.Object.Get<TFeature>();

        public IEnumerator<KeyValuePair<Type, object>> GetEnumerator() => this.Mock.Object.GetEnumerator();

        public void Set<TFeature>(TFeature instance) => this.Mock.Setup(x => x.Get<TFeature>()).Returns(instance);

        IEnumerator IEnumerable.GetEnumerator() => this.Mock.Object.GetEnumerator();
    }
}