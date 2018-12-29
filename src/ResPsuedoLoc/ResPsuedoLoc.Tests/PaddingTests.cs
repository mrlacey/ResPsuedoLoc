// <copyright file="PaddingTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class PaddingTests
    {
        [TestMethod]
        public void EmptyString_Apply()
        {
            var actual = PaddingCommand.PaddingLogic(string.Empty, ToggleMode.Apply);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void EmptyString_Reverse()
        {
            var actual = PaddingCommand.PaddingLogic(string.Empty, ToggleMode.Reverse);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void SingleChar_Apply()
        {
            var actual = PaddingCommand.PaddingLogic("a", ToggleMode.Apply);

            Assert.AreEqual("a", actual);
        }

        [TestMethod]
        public void SingleChar_Reverse()
        {
            var actual = PaddingCommand.PaddingLogic("a", ToggleMode.Reverse);

            Assert.AreEqual("a", actual);
        }

        [TestMethod]
        public void TwoLetters()
        {
            var actual = PaddingCommand.PaddingLogic("ab", ToggleMode.Apply);

            Assert.AreEqual("a-b", actual);
        }

        [TestMethod]
        public void TwoSeparatedLetters()
        {
            var actual = PaddingCommand.PaddingLogic("a-b", ToggleMode.Reverse);

            Assert.AreEqual("ab", actual);
        }

        [TestMethod]
        public void TwoUpperCaseLetters()
        {
            var actual = PaddingCommand.PaddingLogic("AB", ToggleMode.Apply);

            Assert.AreEqual("A-B", actual);
        }

        [TestMethod]
        public void TwoSeparatedUpperCaseLetters()
        {
            var actual = PaddingCommand.PaddingLogic("A-B", ToggleMode.Reverse);

            Assert.AreEqual("AB", actual);
        }

        [TestMethod]
        public void TwoMixedCaseLetters()
        {
            var actual = PaddingCommand.PaddingLogic("Ab", ToggleMode.Apply);

            Assert.AreEqual("A-b", actual);
        }

        [TestMethod]
        public void TwoSeparatedMixedCaseLetters()
        {
            var actual = PaddingCommand.PaddingLogic("a-B", ToggleMode.Reverse);

            Assert.AreEqual("aB", actual);
        }

        [TestMethod]
        public void ThreeLetters()
        {
            var actual = PaddingCommand.PaddingLogic("abc", ToggleMode.Apply);

            Assert.AreEqual("a-b-c", actual);
        }

        [TestMethod]
        public void ThreeSeparatedLetters()
        {
            var actual = PaddingCommand.PaddingLogic("a-b-c", ToggleMode.Reverse);

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void TwoSpaceSeparatedLetters_Apply()
        {
            var actual = PaddingCommand.PaddingLogic("a b", ToggleMode.Apply);

            Assert.AreEqual("a b", actual);
        }

        [TestMethod]
        public void TwoSpaceSeparatedLetters_Reverse()
        {
            var actual = PaddingCommand.PaddingLogic("a b", ToggleMode.Reverse);

            Assert.AreEqual("a b", actual);
        }

        [TestMethod]
        public void HasSeparatorAfterSingleLetter_Apply()
        {
            var actual = PaddingCommand.PaddingLogic("a-", ToggleMode.Apply);

            Assert.AreEqual("a-", actual);
        }

        [TestMethod]
        public void HasSeparatorAfterSingleLetter_Reverse()
        {
            var actual = PaddingCommand.PaddingLogic("a-", ToggleMode.Reverse);

            Assert.AreEqual("a-", actual);
        }

        [TestMethod]
        public void HasSeparatorBeforeSingleLetter_Apply()
        {
            var actual = PaddingCommand.PaddingLogic("-a", ToggleMode.Apply);

            Assert.AreEqual("-a", actual);
        }

        [TestMethod]
        public void HasSeparatorBeforeSingleLetter_Reverse()
        {
            var actual = PaddingCommand.PaddingLogic("-a", ToggleMode.Reverse);

            Assert.AreEqual("-a", actual);
        }

        [TestMethod]
        public void HasSeparatorNotNextToLetters_Apply()
        {
            var actual = PaddingCommand.PaddingLogic("a - b", ToggleMode.Apply);

            Assert.AreEqual("a - b", actual);
        }

        [TestMethod]
        public void HasSeparatorNotNextToLetters_Reverse()
        {
            var actual = PaddingCommand.PaddingLogic("a - b", ToggleMode.Reverse);

            Assert.AreEqual("a - b", actual);
        }

        [TestMethod]
        public void AlreadyHasDoubleSpacedLetters()
        {
            // Double separators are on the limit of scope. Happy to remove both
            var actual = PaddingCommand.PaddingLogic("a--b", ToggleMode.Reverse);

            Assert.AreEqual("ab", actual);
        }

        [TestMethod]
        public void AlreadyHasHyphenatedWordInSentence()
        {
            var actual = PaddingCommand.PaddingLogic("Contains a hyphenated-word in input.", ToggleMode.Apply);

            Assert.AreEqual("C-o-n-t-a-i-n-s a h-y-p-h-e-n-a-t-e-d---w-o-r-d i-n i-n-p-u-t-.", actual);
        }

        [TestMethod]
        public void AlreadyHasHyphenatedWordInSentenceWithPadding()
        {
            var actual = PaddingCommand.PaddingLogic("C-o-n-t-a-i-n-s a h-y-p-h-e-n-a-t-e-d---w-o-r-d i-n i-n-p-u-t-.", ToggleMode.Reverse);

            Assert.AreEqual("Contains a hyphenated-word in input.", actual);
        }

        [TestMethod]
        public void CanPadNonAscii()
        {
            var actual = PaddingCommand.PaddingLogic("äää", ToggleMode.Apply);

            Assert.AreEqual("ä-ä-ä", actual);
        }

        [TestMethod]
        public void CanUnPadNonAscii()
        {
            var actual = PaddingCommand.PaddingLogic("ä-ä-ä", ToggleMode.Reverse);

            Assert.AreEqual("äää", actual);
        }

        [TestMethod]
        public void CanPadCyrillic()
        {
            var actual = PaddingCommand.PaddingLogic("матт", ToggleMode.Apply);

            Assert.AreEqual("м-а-т-т", actual);
        }

        [TestMethod]
        public void CanUnPadCyrillic()
        {
            var actual = PaddingCommand.PaddingLogic("м-а-т-т", ToggleMode.Reverse);

            Assert.AreEqual("матт", actual);
        }

        [TestMethod]
        public void CanPadTextWithCombiningCharacters()
        {
            // 304=macron 308=umlaut
            var actual = PaddingCommand.PaddingLogic("a\u0304\u0308b\u0304\u0308", ToggleMode.Apply);

            Assert.AreEqual("a\u0304\u0308-b\u0304\u0308", actual);
        }

        [TestMethod]
        public void CanUnPadTextWithCombiningCharacters()
        {
            var actual = PaddingCommand.PaddingLogic("a\u0304\u0308-b\u0304\u0308", ToggleMode.Reverse);

            Assert.AreEqual("a\u0304\u0308b\u0304\u0308", actual);
        }

        [TestMethod]
        public void CallingApplyMultipleTimesHasNoEffect()
        {
            var origin = "Original String";

            var once = PaddingCommand.PaddingLogic(origin, ToggleMode.Apply);

            var twice = PaddingCommand.PaddingLogic(origin, ToggleMode.Apply);
            twice = PaddingCommand.PaddingLogic(twice, ToggleMode.Apply);

            Assert.AreEqual(once, twice);
        }

        [TestMethod]
        public void CallingReverseMultipleTimesHasNoEffect()
        {
            var origin = "Original String";

            var once = PaddingCommand.PaddingLogic(origin, ToggleMode.Apply);
            once = PaddingCommand.PaddingLogic(once, ToggleMode.Reverse);

            var twice = PaddingCommand.PaddingLogic(origin, ToggleMode.Apply);
            twice = PaddingCommand.PaddingLogic(twice, ToggleMode.Reverse);
            twice = PaddingCommand.PaddingLogic(twice, ToggleMode.Reverse);

            Assert.AreEqual(once, twice);
        }
    }
}
