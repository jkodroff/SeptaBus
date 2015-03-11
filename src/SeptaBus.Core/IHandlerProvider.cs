using System.Collections.Generic;

namespace SeptaBus
{
    public interface IHandlerProvider
    {
        IHandler<T> GetCommandHandler<T>(T command) where T : ICommand;
        IEnumerable<IHandler<T>> GetEventHandlers<T>(T @event) where T : IEvent;
        IRequestHandler<TReq, TResp> GetRequestHandler<TReq, TResp>(TReq request)
            where TReq : IRequest<TResp>
            where TResp : IResponse;
    }
}