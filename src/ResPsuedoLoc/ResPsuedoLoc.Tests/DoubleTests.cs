// <copyright file="DoubleTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class DoubleTests
    {
        [TestMethod]
        public void MakeDouble()
        {
            var actual = DoubleCommand.DoubleLogic("abc");

            Assert.AreEqual("aabbcc", actual);
        }

        [TestMethod]
        public void RemoveDoubles()
        {
            var actual = DoubleCommand.DoubleLogic("aabbcc");

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void PartialDoublesAreDoubled()
        {
            var actual = DoubleCommand.DoubleLogic("AAbc");

            Assert.AreEqual("AAAAbbcc", actual);
        }

        [TestMethod]
        public void SpacesAreNotDoubled()
        {
            var actual = DoubleCommand.DoubleLogic("A c");

            Assert.AreEqual("AA cc", actual);
        }

        [TestMethod]
        public void SpacesAreIgnoredWhenRemovingDoubles()
        {
            var actual = DoubleCommand.DoubleLogic("AA cc");

            Assert.AreEqual("A c", actual);
        }

        [TestMethod]
        public void SpacesAtStartAndEndAreIgnoredWhenRemovingDoubles()
        {
            var actual = DoubleCommand.DoubleLogic(" AA cc ");

            Assert.AreEqual(" A c ", actual);
        }

        [TestMethod]
        public void HandlesEmptyString()
        {
            var actual = DoubleCommand.DoubleLogic(string.Empty);

            Assert.AreEqual(string.Empty, actual);
        }
    }
}
