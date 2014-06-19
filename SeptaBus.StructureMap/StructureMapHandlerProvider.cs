using System.Collections.Generic;
using StructureMap;

namespace SeptaBus
{
    public class StructureMapHandlerProvider : IHandlerProvider
    {
        public IHandler<T> GetCommandHandler<T>(T command) where T : ICommand
        {
            return ObjectFactory.GetInstance<IHandler<T>>();
        }

        public IEnumerable<IHandler<T>> GetEventHandlers<T>(T @event) where T : IEvent
        {
            return ObjectFactory.GetAllInstances<IHandler<T>>();
        }
    }
}