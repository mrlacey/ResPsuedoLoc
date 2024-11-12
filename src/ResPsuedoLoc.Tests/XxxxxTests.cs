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
            var actual = XxxxxCommand.XxxxxLogic("mrläÄcey");

            Assert.AreEqual("xxxxXxxx", actual);
        }

        [TestMethod]
        public void CanHandleCyrillic()
        {
            var actual = XxxxxCommand.XxxxxLogic("Матт");

            Assert.AreEqual("Xxxx", actual);
        }

        [TestMethod]
        public void CanHandleNull()
        {
            var actual = XxxxxCommand.XxxxxLogic(null);

            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void CanHandleEmptyString()
        {
            var actual = XxxxxCommand.XxxxxLogic(string.Empty);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void CanHandleWhiteSpace()
        {
            var actual = XxxxxCommand.XxxxxLogic(" ");

            Assert.AreEqual(" ", actual);
        }
    }
}
