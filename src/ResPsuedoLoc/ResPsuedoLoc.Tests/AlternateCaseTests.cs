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
    }
}
