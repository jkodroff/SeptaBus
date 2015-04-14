using System;

namespace SeptaBus.Decorators
{
    public class TimestampDecorator : IMessageDecorator
    {
        public void Decorate(IMessage message)
        {
            var headers = message as IHasHeaders;
            if (headers == null)
                return;

            headers.On(DateTime.Now);
        }
    }
}
