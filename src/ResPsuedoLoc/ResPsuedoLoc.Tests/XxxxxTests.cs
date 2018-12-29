// <copyright file="XxxxxTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class XxxxxTests
    {
        [TestMethod]
        public void SimpleFunctionalityWorksOk()
        {
            var source = "Mixed Case Sentence.";

            var actual = XxxxxCommand.XxxxxLogic(source);

            Assert.AreEqual("Xxxxx Xxxx Xxxxxxxx.", actual);
        }

        [TestMethod]
        public void CanHandleNonAscii()
        {
            var actual = XxxxxCommand.XxxxxLogic("mrläcey");

            Assert.AreEqual("xxxxxxx", actual);
        }

        [TestMethod]
        public void CanHandleCyrillic()
        {
            var actual = XxxxxCommand.XxxxxLogic("матт");

            Assert.AreEqual("xxxx", actual);
        }
    }
}
