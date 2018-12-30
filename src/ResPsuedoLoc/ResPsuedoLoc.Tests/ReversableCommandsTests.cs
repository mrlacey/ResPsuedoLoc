// <copyright file="ReversableCommandsTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class ReversableCommandsTests
    {
        [TestMethod]
        public void Surrounded_RemovesWhereHave()
        {
            var origin = new List<string> { "[! surrounded !]", "not surrounded", "[! also surrounded !]" };

            var sut = SurroundCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "surrounded", "not surrounded", "also surrounded" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Surrounded_AddsWhereDoNotHave()
        {
            var origin = new List<string> { "not surrounded", "[! surrounded !]", "also not surrounded" };

            var sut = SurroundCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "[! not surrounded !]", "[! surrounded !]", "[! also not surrounded !]" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Double_RemovesWhereHave()
        {
            var origin = new List<string> { "DDoouubblleedd", "not doubled", "AAllssoo ddoouubblleedd" };

            var sut = DoubleCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "Doubled", "not doubled", "Also doubled" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Double_AddsWhereDoNotHave()
        {
            var origin = new List<string> { "not doubled", "DDoouubblleedd", "also not doubled" };

            var sut = DoubleCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "nnoott ddoouubblleedd", "DDoouubblleedd", "aallssoo nnoott ddoouubblleedd" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Double_WithSurround_RemovesWhereHave()
        {
            var origin = new List<string> { "[! DDoouubblleedd !]", "[! not doubled !]", "[! AAllssoo ddoouubblleedd !]" };

            var sut = DoubleCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "[! Doubled !]", "[! not doubled !]", "[! Also doubled !]" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Double_WithSurround_AddsWhereDoNotHave()
        {
            var origin = new List<string> { "[! not doubled !]", "[! DDoouubblleedd !]", "[! also not doubled !]" };

            var sut = DoubleCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "[! nnoott ddoouubblleedd !]", "[! DDoouubblleedd !]", "[! aallssoo nnoott ddoouubblleedd !]" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Padding_RemovesWhereHave()
        {
            var origin = new List<string> { "P-a-d-d-e-d", "not padded", "A-l-s-o p-a-d-d-e-d" };

            var sut = PaddingCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "Padded", "not padded", "Also padded" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Padding_AddsWhereDoNotHave()
        {
            var origin = new List<string> { "not padded", "P-a-d-d-e-d", "also not padded" };

            var sut = PaddingCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "n-o-t p-a-d-d-e-d", "P-a-d-d-e-d", "a-l-s-o n-o-t p-a-d-d-e-d" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Padding_WithSurround_RemovesWhereHave()
        {
            var origin = new List<string> { "[! P-a-d-d-e-d !]", "[! not padded !]", "[! A-l-s-o p-a-d-d-e-d !]" };

            var sut = PaddingCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "[! Padded !]", "[! not padded !]", "[! Also padded !]" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Padding_WithSurround_AddsWhereDoNotHave()
        {
            var origin = new List<string> { "[! not padded !]", "[! P-a-d-d-e-d !]", "[! also not padded !]" };

            var sut = PaddingCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "[! n-o-t p-a-d-d-e-d !]", "[! P-a-d-d-e-d !]", "[! a-l-s-o n-o-t p-a-d-d-e-d !]" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Diacritics_RemovesWhereHave()
        {
            var origin = new List<string>
            {
                "h\u0346\u033Aa\u0346\u033As\u0346\u033A",
                "does not have",
                "A\u0309\u0321l\u0309\u0321s\u0309\u0321o\u0309\u0321 h\u0309\u0321a\u0309\u0321s\u0309\u0321"
            };

            var sut = DiacriticsCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string> { "has", "does not have", "Also has" };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Diacritics_AddsWhereDoNotHave()
        {
            var origin = new List<string>
            {
                "does not have",
                "h\u0346\u033Aa\u0346\u033As\u0346\u033A",
                "also does not have"
            };

            var sut = DiacriticsCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string>
            {
                "d\u030A\u0325o\u030A\u0325e\u030A\u0325s\u030A\u0325 n\u030A\u0325o\u030A\u0325t\u030A\u0325 h\u030A\u0325a\u030A\u0325v\u030A\u0325e\u030A\u0325",
                "h\u0346\u033Aa\u0346\u033As\u0346\u033A",
                "a\u0308\u0324l\u0308\u0324s\u0308\u0324o\u0308\u0324 d\u0308\u0324o\u0308\u0324e\u0308\u0324s\u0308\u0324 n\u0308\u0324o\u0308\u0324t\u0308\u0324 h\u0308\u0324a\u0308\u0324v\u0308\u0324e\u0308\u0324"
            };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Diacritics_WithSurround_RemovesWhereHave()
        {
            var origin = new List<string>
            {
                "[! h\u0346\u033Aa\u0346\u033As\u0346\u033A !]",
                "[! does not have !]",
                "[! A\u0309\u0321l\u0309\u0321s\u0309\u0321o\u0309\u0321 h\u0309\u0321a\u0309\u0321s\u0309\u0321 !]"
            };

            var sut = DiacriticsCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string>
            {
                "[! has !]",
                "[! does not have !]",
                "[! Also has !]"
            };

            Assert.That.StringListsAreEqual(expected, actual);
        }

        [TestMethod]
        public void Diacritics_WithSurround_AddsWhereDoNotHave()
        {
            var origin = new List<string>
            {
                "[! does not have !]",
                "[! h\u0346\u033Aa\u0346\u033As\u0346\u033A !]",
                "[! also does not have !]"
            };

            var sut = DiacriticsCommand.CreateForTesting();

            var actual = sut.TestActingOnMultipleStrings(origin);

            var expected = new List<string>
            {
                "[! d\u030A\u0325o\u030A\u0325e\u030A\u0325s\u030A\u0325 n\u030A\u0325o\u030A\u0325t\u030A\u0325 h\u030A\u0325a\u030A\u0325v\u030A\u0325e\u030A\u0325 !]",
                "[! h\u0346\u033Aa\u0346\u033As\u0346\u033A !]",
                "[! a\u0308\u0324l\u0308\u0324s\u0308\u0324o\u0308\u0324 d\u0308\u0324o\u0308\u0324e\u0308\u0324s\u0308\u0324 n\u0308\u0324o\u0308\u0324t\u0308\u0324 h\u0308\u0324a\u0308\u0324v\u0308\u0324e\u0308\u0324 !]"
            };

            Assert.That.StringListsAreEqual(expected, actual);
        }
    }
}
