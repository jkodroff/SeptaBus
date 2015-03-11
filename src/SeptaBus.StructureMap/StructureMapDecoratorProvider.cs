using System.Collections.Generic;
using StructureMap;

namespace SeptaBus
{
    public class StructureMapDecoratorProvider : IMessageDecoratorProvider
    {
        private IContainer _container;

        public StructureMapDecoratorProvider(IContainer container)
        {
            _container = container;
        }

        public IEnumerable<IMessageDecorator> GetDecorators()
        {
            return _container.GetAllInstances<IMessageDecorator>();
        }
    }
}