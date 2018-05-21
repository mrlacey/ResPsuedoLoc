// <copyright file="CombinationTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

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

        [TestMethod]
        public void AddDiacrtiticsWhenAlreadyHasPadding()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a-b-c-d-e-f");

            Assert.AreEqual("a\u0306\u032E-b\u0306\u032E-c\u0306\u032E-d\u0306\u032E-e\u0306\u032E-f\u0306\u032E", actual);
        }

        [TestMethod]
        public void RemoveDiacrtiticsWhenAlreadyHasPadding()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0306\u032E-b\u0306\u032E-c\u0306\u032E-d\u0306\u032E-e\u0306\u032E-f\u0306\u032E");

            Assert.AreEqual("a-b-c-d-e-f", actual);
        }

        [TestMethod]
        public void AddDiacrtiticsWhenAlreadyHasSurroundsAndPadding()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("[! a-b-c !]");

            Assert.AreEqual("[! a\u0302\u032D-b\u0302\u032D-c\u0302\u032D !]", actual);
        }

        [TestMethod]
        public void RemoveDiacrtiticsWhenAlreadyHasSurroundsAndPadding()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("[! a\u0302\u032D-b\u0302\u032D-c\u0302\u032D !]");

            Assert.AreEqual("[! a-b-c !]", actual);
        }
    }
}
