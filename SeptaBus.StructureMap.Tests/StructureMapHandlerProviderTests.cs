using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using StructureMap;

namespace SeptaBus
{
    [TestFixture]
    public class StructureMapHandlerProviderTests
    {
        [Test]
        public void GetCommandHandler()
        {
            ObjectFactory.Initialize(x => x.For<IHandler<MyCommand>>().Use<MyCommandHandler>());
            var provider = new StructureMapHandlerProvider();

            provider.GetCommandHandler(new MyCommand()).Should().BeOfType<MyCommandHandler>();
        }

        [Test]
        public void GetEventHandlers()
        {
            ObjectFactory.Initialize(x =>
            {
                x.For<IHandler<MyEvent>>().Use<MyEventHandler1>();
                x.For<IHandler<MyEvent>>().Use<MyEventHandler2>();
            });
            var provider = new StructureMapHandlerProvider();

            var handlers = provider.GetEventHandlers(new MyEvent());
            handlers.Count().Should().Be(2);
            handlers.Should().Contain(x => x is MyEventHandler1);
            handlers.Should().Contain(x => x is MyEventHandler2);
        }

        private class MyEvent : IEvent { }

        private class MyEventHandler1 : IHandler<MyEvent>
        {
            public void Handle(MyEvent args)
            {
                throw new NotImplementedException();
            }
        }

        private class MyEventHandler2 : IHandler<MyEvent>
        {
            public void Handle(MyEvent args)
            {
                throw new NotImplementedException();
            }
        }

        private class MyCommandHandler : IHandler<MyCommand>
        {
            public void Handle(MyCommand args)
            {
                throw new NotImplementedException();
            }
        }

        private class MyCommand : ICommand { }
    }
}