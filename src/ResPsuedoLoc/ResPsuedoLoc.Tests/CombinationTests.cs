using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class CombinationTests
    {
        [TestMethod]
        public void ReverseAndSurround()
        {
            var actual = ReverseCommand.ReverseLogic("[! abc !]");

            Assert.AreEqual("[! cba !]", actual);
        }
    }
}
