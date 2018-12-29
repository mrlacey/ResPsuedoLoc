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
            var actual = DoubleCommand.DoubleLogic("abc", ToggleMode.Apply);

            Assert.AreEqual("aabbcc", actual);
        }

        [TestMethod]
        public void RemoveDoubles()
        {
            var actual = DoubleCommand.DoubleLogic("aabbcc", ToggleMode.Reverse);

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void PartialDoublesAreDoubled()
        {
            var actual = DoubleCommand.DoubleLogic("AAbc", ToggleMode.Apply);

            Assert.AreEqual("AAAAbbcc", actual);
        }

        [TestMethod]
        public void SpacesAreNotDoubled()
        {
            var actual = DoubleCommand.DoubleLogic("A c", ToggleMode.Apply);

            Assert.AreEqual("AA cc", actual);
        }

        [TestMethod]
        public void SpacesAreIgnoredWhenRemovingDoubles()
        {
            var actual = DoubleCommand.DoubleLogic("AA cc", ToggleMode.Reverse);

            Assert.AreEqual("A c", actual);
        }

        [TestMethod]
        public void SpacesAtStartAndEndAreIgnoredWhenRemovingDoubles()
        {
            var actual = DoubleCommand.DoubleLogic(" AA cc ", ToggleMode.Reverse);

            Assert.AreEqual(" A c ", actual);
        }

        [TestMethod]
        public void HandlesEmptyString_Apply()
        {
            var actual = DoubleCommand.DoubleLogic(string.Empty, ToggleMode.Apply);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void HandlesEmptyString_Reverse()
        {
            var actual = DoubleCommand.DoubleLogic(string.Empty, ToggleMode.Reverse);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void CallingApplyMultipleTimesHasNoEffect()
        {
            var origin = "Original String";

            var once = DoubleCommand.DoubleLogic(origin, ToggleMode.Apply);

            var twice = DoubleCommand.DoubleLogic(origin, ToggleMode.Apply);
            twice = DoubleCommand.DoubleLogic(twice, ToggleMode.Apply);

            Assert.AreEqual(once, twice);
        }

        [TestMethod]
        public void CallingReverseMultipleTimesHasNoEffect()
        {
            var origin = "Original String";

            var once = DoubleCommand.DoubleLogic(origin, ToggleMode.Apply);
            once = DoubleCommand.DoubleLogic(once, ToggleMode.Reverse);

            var twice = DoubleCommand.DoubleLogic(origin, ToggleMode.Apply);
            twice = DoubleCommand.DoubleLogic(twice, ToggleMode.Reverse);
            twice = DoubleCommand.DoubleLogic(twice, ToggleMode.Reverse);

            Assert.AreEqual(once, twice);
        }

        [TestMethod]
        public void CanHandleNonAscii()
        {
            var actual = DoubleCommand.DoubleLogic("mrläcey", ToggleMode.Apply);

            Assert.AreEqual("mmrrllääcceeyy", actual);
        }

        [TestMethod]
        public void CanHandleCyrillic()
        {
            var actual = DoubleCommand.DoubleLogic("матт", ToggleMode.Apply);

            Assert.AreEqual("ммаатттт", actual);
        }
    }
}
