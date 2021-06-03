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
            var actual = SurroundCommand.SurroundLogic("abc", ToggleMode.Apply);

            Assert.AreEqual("[! abc !]", actual);
        }

        [TestMethod]
        public void HasBoth()
        {
            var actual = SurroundCommand.SurroundLogic("[! abc !]", ToggleMode.Reverse);

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void AlreadyHasAtStart()
        {
            var actual = SurroundCommand.SurroundLogic("[! abc", ToggleMode.Apply);

            Assert.AreEqual("[! [! abc !]", actual);
        }

        [TestMethod]
        public void AlreadyHasAtEnd()
        {
            var actual = SurroundCommand.SurroundLogic("abc !]", ToggleMode.Apply);

            Assert.AreEqual("[! abc !] !]", actual);
        }

        [TestMethod]
        public void DoNotRemoveIfNotThere()
        {
            var actual = SurroundCommand.SurroundLogic("abc", ToggleMode.Reverse);

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void DoNotAddIfThere()
        {
            var actual = SurroundCommand.SurroundLogic("[! abc !]", ToggleMode.Apply);

            Assert.AreEqual("[! abc !]", actual);
        }

        [TestMethod]
        public void CallingApplyMultipleTimesHasNoEffect()
        {
            var origin = "Original String";

            var once = SurroundCommand.SurroundLogic(origin, ToggleMode.Apply);

            var twice = SurroundCommand.SurroundLogic(origin, ToggleMode.Apply);
            twice = SurroundCommand.SurroundLogic(twice, ToggleMode.Apply);

            Assert.AreEqual(once, twice);
        }

        [TestMethod]
        public void CallingReverseMultipleTimesHasNoEffect()
        {
            var origin = "Original String";

            var once = SurroundCommand.SurroundLogic(origin, ToggleMode.Apply);
            once = SurroundCommand.SurroundLogic(once, ToggleMode.Reverse);

            var twice = SurroundCommand.SurroundLogic(origin, ToggleMode.Apply);
            twice = SurroundCommand.SurroundLogic(twice, ToggleMode.Reverse);
            twice = SurroundCommand.SurroundLogic(twice, ToggleMode.Reverse);

            Assert.AreEqual(once, twice);
        }

        [TestMethod]
        public void CanHandleNull()
        {
            var actual = SurroundCommand.SurroundLogic(null, ToggleMode.Apply);

            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void CanHandleEmptyString()
        {
            var actual = SurroundCommand.SurroundLogic(string.Empty, ToggleMode.Apply);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void CanHandleWhiteSpace()
        {
            var actual = SurroundCommand.SurroundLogic(" ", ToggleMode.Apply);

            Assert.AreEqual(" ", actual);
        }

        [TestMethod]
        public void CanHandleToggleModeNotSet()
        {
            var actual = SurroundCommand.SurroundLogic("Something", ToggleMode.NotSet);

            Assert.AreEqual("Something", actual);
        }
    }
}
