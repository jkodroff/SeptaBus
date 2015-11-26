using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;

namespace SeptaBus
{
    [TestFixture]
    public class BusTests
    {
        [Test]
        public void Publish_CallsHandleOnHandlerProvider()
        {
            var handler1 = new MyEventHandler();
            var handler2 = new MyEventHandler();
            var handlerProvider = new MockHandlerProvider(new[] { handler1, handler2 });

            var bus = new Bus(handlerProvider, new MockDecoratorProvider());
            bus.Publish(new MyEvent());

            handler1.Count.Should().Be(1);
            handler2.Count.Should().Be(1);
        }

        [Test]
        public void Publish_CallsDecorators()
        {
            var decorator1 = new MyDecorator();
            var decorator2 = new MyDecorator();

            var bus = new Bus(new MockHandlerProvider(), new MockDecoratorProvider(new[] { decorator1, decorator2 }));

            bus.Publish(new MyEvent());

            decorator1.Count.Should().Be(1);
            decorator2.Count.Should().Be(1);
        }

        [Test]
        public void Send_Command_NoHandler_ThrowsException()
        {
            var bus = new Bus(new MockHandlerProvider(), new MockDecoratorProvider());

            bus
                .Invoking(x => x.Send(new MyCommand()))
                .ShouldThrow<Exception>().WithMessage("*no handler registered*");
        }

        [Test]
        public void Send_Command_CallsHandler()
        {
            var handler = new MyCommandHandler();
            var handlerProvider = new MockHandlerProvider(null, handler);

            var bus = new Bus(handlerProvider, new MockDecoratorProvider());

            bus.Send(new MyCommand());

            handler.Count.Should().Be(1);
        }

        [Test]
        public void Send_Command_CallsDecorators()
        {
            var decorator1 = new MyDecorator();
            var decorator2 = new MyDecorator();

            var bus = new Bus(
                new MockHandlerProvider(null, new MyCommandHandler()),
                new MockDecoratorProvider(new[] { decorator1, decorator2 })
                );

            bus.Send(new MyCommand());

            decorator1.Count.Should().Be(1);
            decorator2.Count.Should().Be(1);
        }

        [Test]
        public void Send_Request_NoHandler_ThrowsException()
        {
            var bus = new Bus(new MockHandlerProvider(), new MockDecoratorProvider());

            bus
                .Invoking(x => x.Send(new MyCommand()))
                .ShouldThrow<Exception>().WithMessage("*no handler registered*");
        }

        [Test]
        public void Send_Request_CallsHandler()
        {
            var handler = new MyRequestHandler();
            var handlerProvider = new MockHandlerProvider(null, null, handler);

            var bus = new Bus(handlerProvider, new MockDecoratorProvider());

            var resp = bus.Send(new MyRequest());

            resp.Should().NotBeNull();
        }

        [Test]
        public void Send_Request_CallsDecorators()
        {
            var decorator1 = new MyDecorator();
            var decorator2 = new MyDecorator();

            var bus = new Bus(
                new MockHandlerProvider(null, null, new MyRequestHandler()),
                new MockDecoratorProvider(new[] { decorator1, decorator2 })
            );

            bus.Send(new MyRequest());

            decorator1.Count.Should().Be(1);
            decorator2.Count.Should().Be(1);
        }

        private class MyEvent : IEvent { }

        private class MyCommand : ICommand { }

        private class MyRequest : IRequest<MyResponse> { }

        private class MyResponse : IResponse { }

        private class MockHandlerProvider : IHandlerProvider
        {
            private IEnumerable<IHandler<MyEvent>> _eventHandlers;
            private IHandler<MyCommand> _commandHandler;
            private IRequestHandler<MyRequest, MyResponse> _requestHandler;

            public MockHandlerProvider()
            {
                _eventHandlers = new List<IHandler<MyEvent>>();
            }

            public MockHandlerProvider(
                IEnumerable<IHandler<MyEvent>> eventHandlers = null,
                IHandler<MyCommand> commandHandler = null,
                IRequestHandler<MyRequest, MyResponse> requestHandler = null
            ) {
                _eventHandlers = eventHandlers ?? new[] { new MyEventHandler() };
                _commandHandler = commandHandler ?? new MyCommandHandler();
                _requestHandler = requestHandler ?? new MyRequestHandler();
            }

            public IHandler<T> GetCommandHandler<T>(T command) where T : ICommand
            {
                return _commandHandler as IHandler<T>;
            }

            public IEnumerable<IHandler<T>> GetEventHandlers<T>(T @event) where T : IEvent
            {
                if (!(@event is MyEvent))
                    throw new NotImplementedException();

                return _eventHandlers.Cast<IHandler<T>>();
            }

            public IRequestHandler<TReq, TResp> GetRequestHandler<TReq, TResp>(TReq request) where TReq : IRequest<TResp> where TResp : IResponse
            {
                if (!(request is MyRequest))
                    throw new NotImplementedException();
                
                return _requestHandler as IRequestHandler<TReq, TResp>;
            }
        }

        private class MockDecoratorProvider : IMessageDecoratorProvider
        {
            private IEnumerable<IMessageDecorator> _decorators;

            public MockDecoratorProvider(IEnumerable<IMessageDecorator> decorators = null)
            {
                _decorators = decorators;
            }

            public IEnumerable<IMessageDecorator> GetDecorators()
            {
                return _decorators ?? new List<IMessageDecorator>();
            }
        }

        private class MyDecorator : IMessageDecorator
        {
            public int Count;

            public void Decorate(IMessage message)
            {
                Count++;
            }
        }

        private class MyCommandHandler : IHandler<MyCommand>
        {
            public int Count;

            public void Handle(MyCommand message)
            {
                Count++;
            }
        }

        private class MyEventHandler : IHandler<MyEvent>
        {
            public int Count;

            public void Handle(MyEvent message)
            {
                Count++;
            }
        }

        private class MyRequestHandler : IRequestHandler<MyRequest, MyResponse>
        {
            public MyResponse Handle(MyRequest message)
            {
                return new MyResponse();
            }
        }
    }
}