using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class PaddingTests
    {
        [TestMethod]
        public void EmptyString()
        {
            var actual = PaddingCommand.PaddingLogic(string.Empty);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void SingleChar()
        {
            var actual = PaddingCommand.PaddingLogic("a");

            Assert.AreEqual("a", actual);
        }

        [TestMethod]
        public void TwoLetters()
        {
            var actual = PaddingCommand.PaddingLogic("ab");

            Assert.AreEqual("a-b", actual);
        }

        [TestMethod]
        public void TwoSeparatedLetters()
        {
            var actual = PaddingCommand.PaddingLogic("a-b");

            Assert.AreEqual("ab", actual);
        }

        [TestMethod]
        public void TwoUpperCaseLetters()
        {
            var actual = PaddingCommand.PaddingLogic("AB");

            Assert.AreEqual("A-B", actual);
        }

        [TestMethod]
        public void TwoSeparatedUpperCaseLetters()
        {
            var actual = PaddingCommand.PaddingLogic("A-B");

            Assert.AreEqual("AB", actual);
        }

        [TestMethod]
        public void TwoMixedCaseLetters()
        {
            var actual = PaddingCommand.PaddingLogic("Ab");

            Assert.AreEqual("A-b", actual);
        }

        [TestMethod]
        public void TwoSeparatedMixedCaseLetters()
        {
            var actual = PaddingCommand.PaddingLogic("a-B");

            Assert.AreEqual("aB", actual);
        }

        [TestMethod]
        public void ThreeLetters()
        {
            var actual = PaddingCommand.PaddingLogic("abc");

            Assert.AreEqual("a-b-c", actual);
        }

        [TestMethod]
        public void ThreeSeparatedLetters()
        {
            var actual = PaddingCommand.PaddingLogic("a-b-c");

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void TwoSpaceSeparatedLetters()
        {
            var actual = PaddingCommand.PaddingLogic("a b");

            Assert.AreEqual("a b", actual);
        }

        [TestMethod]
        public void HasSeparatorAfterSingleLetter()
        {
            var actual = PaddingCommand.PaddingLogic("a-");

            Assert.AreEqual("a-", actual);
        }

        [TestMethod]
        public void HasSeparatorBeforeSingleLetter()
        {
            var actual = PaddingCommand.PaddingLogic("-a");

            Assert.AreEqual("-a", actual);
        }

        [TestMethod]
        public void HasSeparatorNotNextToLetters()
        {
            var actual = PaddingCommand.PaddingLogic("a - b");

            Assert.AreEqual("a - b", actual);
        }

        [TestMethod]
        public void AlreadyHasDoubleSpacedLetters()
        {
            // Double separators are on the limit of scope. Happy to remove both
            var actual = PaddingCommand.PaddingLogic("a--b");

            Assert.AreEqual("ab", actual);
        }

        [TestMethod]
        public void AlreadyHasHyphenatedWordInSentence()
        {
            var actual = PaddingCommand.PaddingLogic("Contains a hyphenated-word in input.");

            Assert.AreEqual("C-o-n-t-a-i-n-s a h-y-p-h-e-n-a-t-e-d---w-o-r-d i-n i-n-p-u-t-.", actual);
        }

        [TestMethod]
        public void AlreadyHasHyphenatedWordInSentenceWithPadding()
        {
            var actual = PaddingCommand.PaddingLogic("C-o-n-t-a-i-n-s a h-y-p-h-e-n-a-t-e-d---w-o-r-d i-n i-n-p-u-t-.");

            Assert.AreEqual("Contains a hyphenated-word in input.", actual);
        }

        [TestMethod]
        public void CanPadNonAscii()
        {
            var actual = PaddingCommand.PaddingLogic("äää");

            Assert.AreEqual("ä-ä-ä", actual);
        }

        [TestMethod]
        public void CanUnPadNonAscii()
        {
            var actual = PaddingCommand.PaddingLogic("ä-ä-ä");

            Assert.AreEqual("äää", actual);
        }

        [TestMethod]
        public void CanPadCyrillic()
        {
            var actual = PaddingCommand.PaddingLogic("матт");

            Assert.AreEqual("м-а-т-т", actual);
        }

        [TestMethod]
        public void CanUnPadCyrillic()
        {
            var actual = PaddingCommand.PaddingLogic("м-а-т-т");

            Assert.AreEqual("матт", actual);
        }

        [TestMethod]
        public void CanPadTextWithCombiningCharacters()
        {
            // 304=macron 308=umlaut
            var actual = PaddingCommand.PaddingLogic("a\u0304\u0308b\u0304\u0308");

            Assert.AreEqual("a\u0304\u0308-b\u0304\u0308", actual);
        }

        [TestMethod]
        public void CanUnPadTextWithCombiningCharacters()
        {
            var actual = PaddingCommand.PaddingLogic("a\u0304\u0308-b\u0304\u0308");

            Assert.AreEqual("a\u0304\u0308b\u0304\u0308", actual);
        }
    }
}
