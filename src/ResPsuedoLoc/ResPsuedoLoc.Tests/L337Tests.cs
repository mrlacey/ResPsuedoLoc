// <copyright file="L337Tests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class L337Tests
    {
        [TestMethod]
        public void SimpleFunctionalityWorksOk()
        {
            var source = "Mixed Case Sentence.";

            var actual = L337Command.L337Logic(source);

            Assert.AreEqual("M1%3d (453 53n73n(3.", actual);
        }

        [TestMethod]
        public void Alphabet_BothCases()
        {
            var source = "abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var actual = L337Command.L337Logic(source);

            Assert.AreEqual("48(d3f6#1j{lmn0p9r57uvw%y2.48(D3F6#1J{LMN0P9R57UVW%Y2", actual);
        }

        [TestMethod]
        public void CanHandleNull()
        {
            var actual = L337Command.L337Logic(null);

            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void CanHandleEmptyString()
        {
            var actual = L337Command.L337Logic(string.Empty);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void CanHandleWhiteSpace()
        {
            var actual = L337Command.L337Logic(" ");

            Assert.AreEqual(" ", actual);
        }
    }
}
