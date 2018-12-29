// <copyright file="CombinationTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
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
        public void DoubleWhenAlreadyHasDiacritics()
        {
            var actual = DoubleCommand.DoubleLogic("a\u0302\u032Db\u0302\u032Dc\u0302\u032Dd\u0302\u032De\u0302\u032D", ToggleMode.Apply);

            Assert.AreEqual("a\u0302\u032Da\u0302\u032Db\u0302\u032Db\u0302\u032Dc\u0302\u032Dc\u0302\u032Dd\u0302\u032Dd\u0302\u032De\u0302\u032De\u0302\u032D", actual);
        }

        [TestMethod]
        public void DoubleWhenAlreadySurrounded()
        {
            var actual = DoubleCommand.DoubleLogic("[! double me !]", ToggleMode.Apply);

            Assert.AreEqual("[! ddoouubbllee mmee !]", actual);
        }

        [TestMethod]
        public void DoubleWhenAlreadyPadded()
        {
            var actual = DoubleCommand.DoubleLogic("d-o-u-b-l-e m-e", ToggleMode.Apply);

            Assert.AreEqual("d-d-o-o-u-u-b-b-l-l-e-e m-m-e-e", actual);
        }

        [TestMethod]
        public void AddDiacrtiticsWhenAlreadyHasPadding()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a-b-c-d-e-f", ToggleMode.Apply);

            Assert.AreEqual("a\u0306\u032E-b\u0306\u032E-c\u0306\u032E-d\u0306\u032E-e\u0306\u032E-f\u0306\u032E", actual);
        }

        [TestMethod]
        public void RemoveDiacrtiticsWhenAlreadyHasPadding()
        {
            var actual = DiacriticsCommand.DiacriticsLogic(
                "a\u0306\u032E-b\u0306\u032E-c\u0306\u032E-d\u0306\u032E-e\u0306\u032E-f\u0306\u032E",
                ToggleMode.Reverse);

            Assert.AreEqual("a-b-c-d-e-f", actual);
        }

        [TestMethod]
        public void AddDiacrtiticsWhenAlreadyHasSurroundsAndPadding()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("[! a-b-c !]", ToggleMode.Apply);

            Assert.AreEqual("[! a\u0302\u032D-b\u0302\u032D-c\u0302\u032D !]", actual);
        }

        [TestMethod]
        public void RemoveDiacrtiticsWhenAlreadyHasSurroundsAndPadding()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("[! a\u0302\u032D-b\u0302\u032D-c\u0302\u032D !]", ToggleMode.Reverse);

            Assert.AreEqual("[! a-b-c !]", actual);
        }

        [TestMethod]
        public void OrderOfDoubleAndDiacriticsDoesMatter()
        {
            var original = "Original String";

            var doubleFirst = DoubleCommand.DoubleLogic(original, ToggleMode.Apply);
            doubleFirst = DiacriticsCommand.DiacriticsLogic(doubleFirst, ToggleMode.Apply);

            var diacriticsFirst = DiacriticsCommand.DiacriticsLogic(original, ToggleMode.Apply);
            diacriticsFirst = DoubleCommand.DoubleLogic(diacriticsFirst, ToggleMode.Apply);

            // This is because the diacritic used is based on the string length
            Assert.AreNotEqual(doubleFirst, diacriticsFirst);
        }

        [TestMethod]
        public void OrderOfDoubleAndDiacriticsDoesMatterButCanRemoveInEitherOrder()
        {
            var original = "Original String";

            // Apply both changes
            var doubleFirst = DoubleCommand.DoubleLogic(original, ToggleMode.Apply);
            doubleFirst = DiacriticsCommand.DiacriticsLogic(doubleFirst, ToggleMode.Apply);

            var diacriticsFirst = DiacriticsCommand.DiacriticsLogic(original, ToggleMode.Apply);
            diacriticsFirst = DoubleCommand.DoubleLogic(diacriticsFirst, ToggleMode.Apply);

            // Remove doubling first
            var doubleFirstRemoved = DoubleCommand.DoubleLogic(doubleFirst, ToggleMode.Reverse);
            doubleFirstRemoved = DiacriticsCommand.DiacriticsLogic(doubleFirstRemoved, ToggleMode.Reverse);

            var diacriticsFirstRemoved = DoubleCommand.DoubleLogic(diacriticsFirst, ToggleMode.Reverse);
            diacriticsFirstRemoved = DiacriticsCommand.DiacriticsLogic(diacriticsFirstRemoved, ToggleMode.Reverse);

            Assert.AreEqual(original, doubleFirstRemoved);
            Assert.AreEqual(original, diacriticsFirstRemoved);

            // Remove diacritics first
            doubleFirstRemoved = DiacriticsCommand.DiacriticsLogic(doubleFirst, ToggleMode.Reverse);
            doubleFirstRemoved = DoubleCommand.DoubleLogic(doubleFirstRemoved, ToggleMode.Reverse);

            diacriticsFirstRemoved = DiacriticsCommand.DiacriticsLogic(diacriticsFirst, ToggleMode.Reverse);
            diacriticsFirstRemoved = DoubleCommand.DoubleLogic(diacriticsFirstRemoved, ToggleMode.Reverse);

            Assert.AreEqual(original, doubleFirstRemoved);
            Assert.AreEqual(original, diacriticsFirstRemoved);
        }

        [TestMethod]
        public void RemoveDoubleWhenAlsoHasDiacritics()
        {
            var actual = DoubleCommand.DoubleLogic(
                "a\u0306\u032Ea\u0306\u032Eb\u0306\u032Eb\u0306\u032Ec\u0306\u032Ec\u0306\u032Ed\u0306\u032Ed\u0306\u032Ee\u0306\u032Ee\u0306\u032E",
                ToggleMode.Reverse);

            Assert.AreEqual("a\u0306\u032Eb\u0306\u032Ec\u0306\u032Ed\u0306\u032Ee\u0306\u032E", actual);
        }

        [TestMethod]
        public void RemoveDoubleWhenAlsoHasDiacritics_ButFirstCharIsNotALetter()
        {
            var actual = DoubleCommand.DoubleLogic(
                ">a\u0306\u032Ea\u0306\u032Eb\u0306\u032Eb\u0306\u032Ec\u0306\u032Ec\u0306\u032Ed\u0306\u032Ed\u0306\u032Ee\u0306\u032Ee\u0306\u032E",
                ToggleMode.Reverse);

            Assert.AreEqual(">a\u0306\u032Eb\u0306\u032Ec\u0306\u032Ed\u0306\u032Ee\u0306\u032E", actual);
        }

        [TestMethod]
        public void RemoveDiacriticsWhenAlsoHasBeenDoubled()
        {
            var actual = DiacriticsCommand.DiacriticsLogic(
                "a\u0302\u032Da\u0302\u032Db\u0302\u032Db\u0302\u032Dc\u0302\u032Dc\u0302\u032Dd\u0302\u032Dd\u0302\u032De\u0302\u032De\u0302\u032D",
                ToggleMode.Reverse);

            Assert.AreEqual("aabbccddee", actual);
        }

        [TestMethod]
        public void RemoveDoublingWhenHasAlsoBeenPadded()
        {
            var actual = DoubleCommand.DoubleLogic("a-a-b-b", ToggleMode.Reverse);

            Assert.AreEqual("a-b", actual);
        }

        [TestMethod]
        public void RemovePaddingWhenHasAlsoBeenDoubled()
        {
            var origin = "ab";

            var actual = PaddingCommand.PaddingLogic(origin, ToggleMode.Apply);
            actual = DoubleCommand.DoubleLogic(actual, ToggleMode.Apply);
            actual = PaddingCommand.PaddingLogic(actual, ToggleMode.Reverse);

            Assert.AreEqual("aabb", actual);
        }

        [TestMethod]
        public void AddPadding_WhereAlreadyHasSurrounds()
        {
            var origin = "Original String";

            var withSurrounds = SurroundCommand.SurroundLogic(origin, ToggleMode.Apply);
            var withPadding = PaddingCommand.PaddingLogic(withSurrounds, ToggleMode.Apply);

            Assert.AreEqual("[! O-r-i-g-i-n-a-l S-t-r-i-n-g !]", withPadding);
        }

        [TestMethod]
        public void CanAddAndRemoveToggleActionsInDifferentOrder()
        {
            var origin = "Original String";

            var modifiedStep1 = SurroundCommand.SurroundLogic(origin, ToggleMode.Apply);
            var modifiedStep2 = DiacriticsCommand.DiacriticsLogic(modifiedStep1, ToggleMode.Apply);
            var modifiedStep3 = DoubleCommand.DoubleLogic(modifiedStep2, ToggleMode.Apply);
            var modifiedStep4 = ReverseCommand.ReverseLogic(modifiedStep3);
            var modifiedStep5 = PaddingCommand.PaddingLogic(modifiedStep4, ToggleMode.Apply);
            var modifiedStep6 = InvertCaseCommand.InvertCaseLogic(modifiedStep5);
            var modifiedStep7 = DiacriticsCommand.DiacriticsLogic(modifiedStep6, ToggleMode.Reverse);
            var modifiedStep8 = SurroundCommand.SurroundLogic(modifiedStep7, ToggleMode.Reverse);
            var modifiedStep9 = DoubleCommand.DoubleLogic(modifiedStep8, ToggleMode.Reverse);
            var modifiedStep10 = ReverseCommand.ReverseLogic(modifiedStep9);
            var modifiedStep11 = InvertCaseCommand.InvertCaseLogic(modifiedStep10);
            var finalResult = PaddingCommand.PaddingLogic(modifiedStep11, ToggleMode.Reverse);

            Assert.AreEqual(origin, finalResult);
        }
    }
}
