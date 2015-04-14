using FluentAssertions;
using NUnit.Framework;

namespace SeptaBus.Decorators
{
    [TestFixture]
    public class UserNameDecoratorTests
    {
        private static string User = "jkodroff";

        [Test]
        public void Decorate()
        {
            var message = new MyMessage();
            new UserNameDecorator(new UserNameProvider()).Decorate(message);
            message.By().Should().Be(User);
        }
        
        private class UserNameProvider : IUserNameProvider
        {
            public string UserName()
            {
                return User;
            }
        }

        private class MyMessage : MessageBase { }
    }
}