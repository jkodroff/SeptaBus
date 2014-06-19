using System.Collections.Generic;

namespace SeptaBus
{
    public interface IHandlerProvider
    {
        IHandler<T> GetCommandHandler<T>(T command) where T : ICommand;
        IEnumerable<IHandler<T>> GetEventHandlers<T>(T @event) where T : IEvent;
    }
}