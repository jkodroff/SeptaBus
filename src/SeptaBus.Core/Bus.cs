using System;
using System.Linq;
using System.Reflection;

namespace SeptaBus
{
    public class Bus : IBus
    {
        private IHandlerProvider _handlerProvider;
        private IMessageDecoratorProvider _decoratorProvider;

        public Bus(IHandlerProvider handlerProvider, IMessageDecoratorProvider decoratorProvider)
        {
            _handlerProvider = handlerProvider;
            _decoratorProvider = decoratorProvider;
        }

        public void Send(params ICommand[] commands)
        {
            if (commands == null || !commands.Any())
                return;

            var sendInternal = typeof (Bus)
                .GetMethod("SendInternal", BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var command in commands)
            {
                RunDecorators(command);

                try
                {
                    sendInternal
                        .MakeGenericMethod(new[] {command.GetType()})
                        .Invoke(this, new[] {command});
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        public void Publish(params IEvent[] events)
        {
            if (events == null || !events.Any())
                return;

            var publishInternal =
                typeof (Bus)
                    .GetMethod("PublishInternal", BindingFlags.Instance | BindingFlags.NonPublic);

            foreach (var @event in events)
            {
                RunDecorators(@event);

                try
                {
                    publishInternal
                        .MakeGenericMethod(new[] {@event.GetType()})
                        .Invoke(this, new[] {@event});
                }
                catch (TargetInvocationException ex)
                {
                    throw ex.InnerException;
                }
            }
        }

        public TResp Send<TResp>(IRequest<TResp> request) where TResp : IResponse
        {
            if (request == null)
                throw new ArgumentNullException("request");

            var sendInternal = typeof(Bus)
                .GetMethod("SendRequestInternal", BindingFlags.Instance | BindingFlags.NonPublic);

            RunDecorators(request);

            try
            {
                return (TResp)sendInternal
                    .MakeGenericMethod(new[] { request.GetType(), typeof(TResp) })
                    .Invoke(this, new[] { request });
            }
            catch (TargetInvocationException ex)
            {
                throw ex.InnerException;
            }
        }

        private void RunDecorators(IMessage message)
        {
            foreach (var decorator in _decoratorProvider.GetDecorators())
            {
                decorator.Decorate(message);
            }
        }

        // Invoked via reflection, so:
        // ReSharper disable once UnusedMember.Local
        private void SendInternal<TCommand>(TCommand command) where TCommand : class, ICommand
        {
            var handler = _handlerProvider.GetCommandHandler(command);
            if (handler == null)
                throw new Exception(string.Format("There is no handler registered for the command of type '{0}'.",
                    command.GetType()));

            handler.Handle(command);
        }

        // Invoked via reflection, so:
        // ReSharper disable once UnusedMember.Local
        private TResp SendRequestInternal<TReq, TResp>(TReq command)
            where TReq : IRequest<TResp>
            where TResp : IResponse
        {
            var handler = _handlerProvider.GetRequestHandler<TReq, TResp>(command);
            if (handler == null)
                throw new Exception(string.Format("There is no handler registered for the request of type '{0}'.",
                    command.GetType()));

            return handler.Handle(command);
        }

        // Invoked via reflection, so:
        // ReSharper disable once UnusedMember.Local
        private void PublishInternal<TEvent>(TEvent @event) where TEvent : class, IEvent
        {
            var handlers = _handlerProvider.GetEventHandlers(@event);
            foreach (var handler in handlers)
            {
                handler.Handle(@event);
            }
        }
    }
}