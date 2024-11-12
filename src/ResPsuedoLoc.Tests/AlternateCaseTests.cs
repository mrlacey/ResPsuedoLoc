// <copyright file="AlternateCaseTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class AlternateCaseTests
    {
        [TestMethod]
        public void SimpleFunctionalityWorksOk()
        {
            var source = "Mixed Case Sentence.";

            var actual = AlternateCaseCommand.AlternateCaseLogic(source);

            Assert.AreEqual("MiXeD cAsE sEnTeNcE.", actual);
        }

        [TestMethod]
        public void Alphabet_LowerCase()
        {
            var source = "abcdefghijklmnopqrstuvwxyz";

            var actual = AlternateCaseCommand.AlternateCaseLogic(source);

            Assert.AreEqual("AbCdEfGhIjKlMnOpQrStUvWxYz", actual);
        }

        [TestMethod]
        public void Alphabet_UpperCase()
        {
            var source = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var actual = AlternateCaseCommand.AlternateCaseLogic(source);

            Assert.AreEqual("AbCdEfGhIjKlMnOpQrStUvWxYz", actual);
        }

        [TestMethod]
        public void CanHandleNonAscii()
        {
            var actual = AlternateCaseCommand.AlternateCaseLogic("mrlääcey");

            Assert.AreEqual("MrLäÄcEy", actual);
        }

        [TestMethod]
        public void CanHandleCyrillic()
        {
            var actual = AlternateCaseCommand.AlternateCaseLogic("матт");

            Assert.AreEqual("МаТт", actual);
        }

        [TestMethod]
        public void CanHandleNull()
        {
            var actual = AlternateCaseCommand.AlternateCaseLogic(null);

            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void CanHandleEmptyString()
        {
            var actual = AlternateCaseCommand.AlternateCaseLogic(string.Empty);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void CanHandleWhiteSpace()
        {
            var actual = AlternateCaseCommand.AlternateCaseLogic(" ");

            Assert.AreEqual(" ", actual);
        }
    }
}
