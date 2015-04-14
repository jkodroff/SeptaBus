using System.Linq;
using NUnit.Framework;

namespace SeptaBus
{
    [TestFixture]
    public class MessageBaseTests
    {
        public class MyCommand : MessageBase { }

        private MyCommand command;

        [SetUp]
        public void SetUp()
        {
            command = new MyCommand();
            command.SetHeader("currentUser", "testUser");
            command.SetHeader("otherHeader", "otherValue");
        }

        [Test]
        public void GetHeader_RetrievesHeaderValue()
        {
            Assert.AreEqual("testUser", command.GetHeader("currentUser"));
            Assert.AreEqual("otherValue", command.GetHeader("otherHeader"));
        }

        [Test]
        public void GetHeaderKeys_ReturnsListOfHeaderKeys()
        {
            var keys = command.GetHeaderKeys().ToList();
            Assert.Contains("currentUser", keys);
            Assert.Contains("otherHeader", keys);
        }
    }
}