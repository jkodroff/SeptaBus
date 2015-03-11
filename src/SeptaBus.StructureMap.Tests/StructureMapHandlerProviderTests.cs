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
            new StructureMapHandlerProvider(
                    new Container(x => x.For<IHandler<MyCommand>>().Use<MyCommandHandler>())
                )
                .GetCommandHandler(new MyCommand()).Should().BeOfType<MyCommandHandler>();
        }

        [Test]
        public void GetEventHandlers()
        {
            var handlers = 
                new StructureMapHandlerProvider(
                    new Container(x => {
                        x.For<IHandler<MyEvent>>().Use<MyEventHandler1>();
                        x.For<IHandler<MyEvent>>().Use<MyEventHandler2>();
                    })
                )
                .GetEventHandlers(new MyEvent());
            
            handlers.Count().Should().Be(2);
            handlers.Should().Contain(x => x is MyEventHandler1);
            handlers.Should().Contain(x => x is MyEventHandler2);
        }

        [Test]
        public void GetRequestHandler()
        {
            new StructureMapHandlerProvider(
                new Container(x => 
                    x.For<IRequestHandler<MyRequest, MyResponse>>()
                        .Use<MyRequestHandler>())
            )
            .GetRequestHandler<MyRequest, MyResponse>(new MyRequest())
            .Should().BeOfType<MyRequestHandler>();
        }

        private class MyEvent : IEvent { }

        private class MyEventHandler1 : IHandler<MyEvent>
        {
            public void Handle(MyEvent message)
            {
                throw new NotImplementedException();
            }
        }

        private class MyEventHandler2 : IHandler<MyEvent>
        {
            public void Handle(MyEvent message)
            {
                throw new NotImplementedException();
            }
        }

        private class MyCommandHandler : IHandler<MyCommand>
        {
            public void Handle(MyCommand message)
            {
                throw new NotImplementedException();
            }
        }

        private class MyCommand : ICommand { }

        private class MyRequest : IRequest<MyResponse> { }

        private class MyResponse : IResponse { }
        
        private class MyRequestHandler : IRequestHandler<MyRequest, MyResponse>
        {
            public MyResponse Handle(MyRequest message)
            {
                throw new NotImplementedException();
            }
        }
    }
}