using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class ReverseTests
    {
        [TestMethod]
        public void SimpleAscii()
        {
            var actual = ReverseCommand.ReverseLogic("abc");

            Assert.AreEqual("cba", actual);
        }

        // TODO: add tests for strings using combining characters and other character sets
    }
}
