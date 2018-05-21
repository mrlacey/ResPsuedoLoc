// <copyright file="ReverseTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

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

        [TestMethod]
        public void WithCombiningCharacters()
        {
            var actual = ReverseCommand.ReverseLogic("a\u0346bc");

            Assert.AreEqual("cba\u0346", actual);
        }

        [TestMethod]
        public void WithNonAscii()
        {
            var actual = ReverseCommand.ReverseLogic("mrläcey");

            Assert.AreEqual("yecälrm", actual);
        }

        [TestMethod]
        public void WithCyrillic()
        {
            var actual = ReverseCommand.ReverseLogic("матт");

            Assert.AreEqual("ттам", actual);
        }
    }
}
