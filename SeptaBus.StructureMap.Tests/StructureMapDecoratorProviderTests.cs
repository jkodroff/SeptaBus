using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using StructureMap;

namespace SeptaBus
{
    [TestFixture]
    public class StructureMapDecoratorProviderTests
    {
        [Test]
        public void GetDecorators()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<IMessageDecorator>().Add<MyDecorator1>();
                x.For<IMessageDecorator>().Add<MyDecorator2>();
            });

            var provider = new StructureMapDecoratorProvider();

            var decorators = provider.GetDecorators();

            decorators.Should().Contain(x => x is MyDecorator1);
            decorators.Should().Contain(x => x is MyDecorator2);
            decorators.Count().Should().Be(2);
        }

        private class MyDecorator1 : IMessageDecorator
        {
            public void Decorate(IMessage message)
            {
                throw new NotImplementedException();
            }
        }

        private class MyDecorator2 : IMessageDecorator
        {
            public void Decorate(IMessage message)
            {
                throw new NotImplementedException();
            }
        }
    }
}