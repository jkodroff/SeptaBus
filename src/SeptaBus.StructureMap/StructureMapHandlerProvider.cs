using System.Collections.Generic;
using StructureMap;

namespace SeptaBus
{
    public class StructureMapHandlerProvider : IHandlerProvider
    {
        private IContainer _container;

        public StructureMapHandlerProvider(IContainer container)
        {
            _container = container;
        }

        public IHandler<T> GetCommandHandler<T>(T command) where T : ICommand
        {
            return _container.GetInstance<IHandler<T>>();
        }

        public IEnumerable<IHandler<T>> GetEventHandlers<T>(T @event) where T : IEvent
        {
            return _container.GetAllInstances<IHandler<T>>();
        }

        public IRequestHandler<TReq, TResp> GetRequestHandler<TReq, TResp>(TReq request) where TReq : IRequest<TResp> where TResp : IResponse
        {
            return _container.GetInstance<IRequestHandler<TReq, TResp>>();
        }
    }
}