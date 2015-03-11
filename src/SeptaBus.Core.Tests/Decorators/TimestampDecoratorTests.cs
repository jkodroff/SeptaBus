using System;
using FluentAssertions;
using NUnit.Framework;

namespace SeptaBus.Decorators
{
    [TestFixture]
    public class TimestampDecoratorTests
    {
        [Test]
        public void Decorate()
        {
            var message = new MyMessage();
            new TimestampDecorator().Decorate(message);
            message.On().Should().BeCloseTo(DateTime.Now);
        }
        
        private class MyMessage : MessageBase { }
    }
}
