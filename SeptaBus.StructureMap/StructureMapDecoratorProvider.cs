using System.Collections.Generic;
using StructureMap;

namespace SeptaBus
{
    public class StructureMapDecoratorProvider : IMessageDecoratorProvider
    {
        public IEnumerable<IMessageDecorator> GetDecorators()
        {
            return ObjectFactory.GetAllInstances<IMessageDecorator>();
        }
    }
}