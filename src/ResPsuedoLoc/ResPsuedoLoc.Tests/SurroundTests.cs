// <copyright file="SurroundTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class SurroundTests
    {
        [TestMethod]
        public void HasNone()
        {
            var actual = SurroundCommand.SurroundLogic("abc");

            Assert.AreEqual("[! abc !]", actual);
        }

        [TestMethod]
        public void HasBoth()
        {
            var actual = SurroundCommand.SurroundLogic("[! abc !]");

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void AlreadyHasAtStart()
        {
            var actual = SurroundCommand.SurroundLogic("[! abc");

            Assert.AreEqual("[! [! abc !]", actual);
        }

        [TestMethod]
        public void AlreadyHasAtEnd()
        {
            var actual = SurroundCommand.SurroundLogic("abc !]");

            Assert.AreEqual("[! abc !] !]", actual);
        }
    }
}
