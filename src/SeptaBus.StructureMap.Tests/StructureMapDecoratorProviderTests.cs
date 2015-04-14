using System;
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
            new StructureMapDecoratorProvider(
                    new Container(x => {
                        x.For<IMessageDecorator>().Add<MyDecorator1>();
                        x.For<IMessageDecorator>().Add<MyDecorator2>();
                    })
                )
                .GetDecorators()
                .Should().Contain(x => x is MyDecorator1)
                .And.Contain(x => x is MyDecorator2)
                .And.HaveCount(2);
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