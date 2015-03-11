using System.Collections.Generic;

namespace SeptaBus
{
    public interface IMessageDecoratorProvider
    {
        IEnumerable<IMessageDecorator> GetDecorators();
    }
}