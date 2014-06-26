using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;

namespace SeptaBus.Decorators
{
    [TestFixture]
    public class RolesDecoratorTests
    {
        private static IEnumerable<string> Roles = new[] {"Users", "Administrators"};

        [Test]
        public void Decorate()
        {
            var message = new MyMessage();
            new RolesDecorator(new RolesProvider()).Decorate(message);
            message.Roles().Should().BeEquivalentTo(Roles);
        }

        private class RolesProvider : IRolesProvider
        {
            public IEnumerable<string> CurrentUsersRoles()
            {
                return Roles;
            }
        }
        
        private class MyMessage : MessageBase { }
    }
}