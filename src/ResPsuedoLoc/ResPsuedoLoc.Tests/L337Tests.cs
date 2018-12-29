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

            Assert.AreEqual("M1x3d C453 53n73nc3.", actual);
        }

        [TestMethod]
        public void Alphabet_BothCases()
        {
            var source = "abcdefghijklmnopqrstuvwxyz.ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var actual = L337Command.L337Logic(source);

            Assert.AreEqual("48cd3f6h1jklmn0p9r57uvwxy2.48CD3F6H1JKLMN0P9R57UVWXY2", actual);
        }
    }
}
