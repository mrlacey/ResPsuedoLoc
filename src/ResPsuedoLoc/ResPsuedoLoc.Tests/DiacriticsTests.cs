﻿// <copyright file="DiacriticsTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class DiacriticsTests
    {
        [TestMethod]
        public void AddToStringOfLength1_Equals()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a", ToggleMode.Apply);

            Assert.AreEqual("a\u033F\u0347", actual);
            Assert.AreEqual("a͇̿", actual);
        }

        [TestMethod]
        public void AddToStringOfLength2_Breve()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("ab", ToggleMode.Apply);

            Assert.AreEqual("a\u0306\u032Eb\u0306\u032E", actual);
            Assert.AreEqual("ă̮b̮̆", actual);
        }

        [TestMethod]
        public void AddToStringOfLength3_Bridge()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abc", ToggleMode.Apply);

            Assert.AreEqual("a\u0346\u033Ab\u0346\u033Ac\u0346\u033A", actual);
            Assert.AreEqual("a̺͆b̺͆c̺͆", actual);
        }

        [TestMethod]
        public void AddToStringOfLength4_Ring()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcd", ToggleMode.Apply);

            Assert.AreEqual("a\u030A\u0325b\u030A\u0325c\u030A\u0325d\u030A\u0325", actual);
            Assert.AreEqual("ḁ̊b̥̊c̥̊d̥̊", actual);
        }

        [TestMethod]
        public void AddToStringOfLength5_Circumflex()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcde", ToggleMode.Apply);

            Assert.AreEqual("a\u0302\u032Db\u0302\u032Dc\u0302\u032Dd\u0302\u032De\u0302\u032D", actual);
            Assert.AreEqual("â̭b̭̂ĉ̭ḓ̂ḙ̂", actual);
        }

        [TestMethod]
        public void AddToStringOfLength6_Grave()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdef", ToggleMode.Apply);

            Assert.AreEqual("a\u0300\u0316b\u0300\u0316c\u0300\u0316d\u0300\u0316e\u0300\u0316f\u0300\u0316", actual);
            Assert.AreEqual("à̖b̖̀c̖̀d̖̀è̖f̖̀", actual);
        }

        [TestMethod]
        public void AddToStringOfLength7_Acute()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdefg", ToggleMode.Apply);

            Assert.AreEqual("a\u0301\u0317b\u0301\u0317c\u0301\u0317d\u0301\u0317e\u0301\u0317f\u0301\u0317g\u0301\u0317", actual);
            Assert.AreEqual("á̗b̗́ć̗d̗́é̗f̗́ǵ̗", actual);
        }

        [TestMethod]
        public void AddToStringOfLength8_Hook()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdefgh", ToggleMode.Apply);

            Assert.AreEqual("a\u0309\u0321b\u0309\u0321c\u0309\u0321d\u0309\u0321e\u0309\u0321f\u0309\u0321g\u0309\u0321h\u0309\u0321", actual);
            Assert.AreEqual("ả̡b̡̉c̡̉d̡̉ẻ̡f̡̉g̡̉h̡̉", actual);
        }

        [TestMethod]
        public void AddToStringOfLength9_Umlaut()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdefghi", ToggleMode.Apply);

            Assert.AreEqual("a\u0308\u0324b\u0308\u0324c\u0308\u0324d\u0308\u0324e\u0308\u0324f\u0308\u0324g\u0308\u0324h\u0308\u0324i\u0308\u0324", actual);
            Assert.AreEqual("ä̤b̤̈c̤̈d̤̈ë̤f̤̈g̤̈ḧ̤ï̤", actual);
        }

        [TestMethod]
        public void AddToStringOfLength10_Equals()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdefghij", ToggleMode.Apply);

            Assert.AreEqual("a\u033F\u0347b\u033F\u0347c\u033F\u0347d\u033F\u0347e\u033F\u0347f\u033F\u0347g\u033F\u0347h\u033F\u0347i\u033F\u0347j\u033F\u0347", actual);
            Assert.AreEqual("a͇̿b͇̿c͇̿d͇̿e͇̿f͇̿g͇̿h͇̿i͇̿j͇̿", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength1()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u033F\u0347", ToggleMode.Reverse);

            Assert.AreEqual("a", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength2()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0306\u032Eb\u0306\u032E", ToggleMode.Reverse);

            Assert.AreEqual("ab", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength3()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0346\u033Ab\u0346\u033Ac\u0346\u033A", ToggleMode.Reverse);

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength4()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u030A\u0325b\u030A\u0325c\u030A\u0325d\u030A\u0325", ToggleMode.Reverse);

            Assert.AreEqual("abcd", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength5()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0302\u032Db\u0302\u032Dc\u0302\u032Dd\u0302\u032De\u0302\u032D", ToggleMode.Reverse);

            Assert.AreEqual("abcde", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength6()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0300\u0316b\u0300\u0316c\u0300\u0316d\u0300\u0316e\u0300\u0316f\u0300\u0316", ToggleMode.Reverse);

            Assert.AreEqual("abcdef", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength7()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0301\u0317b\u0301\u0317c\u0301\u0317d\u0301\u0317e\u0301\u0317f\u0301\u0317g\u0301\u0317", ToggleMode.Reverse);

            Assert.AreEqual("abcdefg", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength8()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0309\u0321b\u0309\u0321c\u0309\u0321d\u0309\u0321e\u0309\u0321f\u0309\u0321g\u0309\u0321h\u0309\u0321", ToggleMode.Reverse);

            Assert.AreEqual("abcdefgh", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength9()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0308\u0324b\u0308\u0324c\u0308\u0324d\u0308\u0324e\u0308\u0324f\u0308\u0324g\u0308\u0324h\u0308\u0324i\u0308\u0324", ToggleMode.Reverse);

            Assert.AreEqual("abcdefghi", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength10()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u033F\u0347b\u033F\u0347c\u033F\u0347d\u033F\u0347e\u033F\u0347f\u033F\u0347g\u033F\u0347h\u033F\u0347i\u033F\u0347j\u033F\u0347", ToggleMode.Reverse);

            Assert.AreEqual("abcdefghij", actual);
        }

        [TestMethod]
        public void AddToStringSkipsSpaces()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("ab de", ToggleMode.Apply);

            Assert.AreEqual("a\u0302\u032Db\u0302\u032D d\u0302\u032De\u0302\u032D", actual);
        }

        [TestMethod]
        public void RemovingFromStringSkipsSpaces()
        {
            var actual = DiacriticsCommand.DiacriticsLogic(" a\u0302\u032Db\u0302\u032D d\u0302\u032De\u0302\u032D ", ToggleMode.Reverse);

            Assert.AreEqual(" ab de ", actual);
        }

        [TestMethod]
        public void CallingApplyMultipleTimesHasNoEffect()
        {
            var origin = "Original String";

            var once = DiacriticsCommand.DiacriticsLogic(origin, ToggleMode.Apply);

            var twice = DiacriticsCommand.DiacriticsLogic(origin, ToggleMode.Apply);
            twice = DiacriticsCommand.DiacriticsLogic(twice, ToggleMode.Apply);

            Assert.AreEqual(once, twice);
        }

        [TestMethod]
        public void CallingReverseMultipleTimesHasNoEffect()
        {
            var origin = "Original String";

            var once = DiacriticsCommand.DiacriticsLogic(origin, ToggleMode.Apply);
            once = DiacriticsCommand.DiacriticsLogic(once, ToggleMode.Reverse);

            var twice = DiacriticsCommand.DiacriticsLogic(origin, ToggleMode.Apply);
            twice = DiacriticsCommand.DiacriticsLogic(twice, ToggleMode.Reverse);
            twice = DiacriticsCommand.DiacriticsLogic(twice, ToggleMode.Reverse);

            Assert.AreEqual(once, twice);
        }

        [TestMethod]
        public void CanHandleNonAscii()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("mrläcey", ToggleMode.Apply);

            Assert.AreEqual("m\u0301\u0317r\u0301\u0317l\u0301\u0317ä\u0301\u0317c\u0301\u0317e\u0301\u0317y\u0301\u0317", actual);
            Assert.AreEqual("ḿ̗ŕ̗ĺ̗ä̗́ć̗é̗ý̗", actual);
        }

        [TestMethod]
        public void CanHandleCyrillic()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("матт", ToggleMode.Apply);

            Assert.AreEqual("м\u030A\u0325а\u030A\u0325т\u030A\u0325т\u030A\u0325", actual);
            Assert.AreEqual("м̥̊а̥̊т̥̊т̥̊", actual);
        }

        [TestMethod]
        public void CanDetectWhenAddedToNonLetters()
        {
            var actual = DiacriticsCommand.HasAddedDiacritics("{\u0306\u032E}\u0306\u032E");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CanDetectWhenNotAddedToNonLetters()
        {
            var actual = DiacriticsCommand.HasAddedDiacritics("{}");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CanDetectWhenAddedToNumbers()
        {
            var actual = DiacriticsCommand.HasAddedDiacritics("1\u0306\u032E2\u0306\u032E");

            Assert.IsTrue(actual);
        }

        [TestMethod]
        public void CanDetectWhenNotAddedToNumbers()
        {
            var actual = DiacriticsCommand.HasAddedDiacritics("12");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void TryRemovalWhenHasOnlyOneOfAddedDiacritics_First()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u033F", ToggleMode.Reverse);

            Assert.AreEqual("a\u033F", actual);
        }

        [TestMethod]
        public void TryRemovalWhenHasOnlyOneOfAddedDiacritics_Second()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0347", ToggleMode.Reverse);

            Assert.AreEqual("a\u0347", actual);
        }

        [TestMethod]
        public void CanHandleNull()
        {
            var actual = DiacriticsCommand.DiacriticsLogic(null, ToggleMode.Apply);

            Assert.AreEqual(null, actual);
        }

        [TestMethod]
        public void CanHandleEmptyString()
        {
            var actual = DiacriticsCommand.DiacriticsLogic(string.Empty, ToggleMode.Apply);

            Assert.AreEqual(string.Empty, actual);
        }

        [TestMethod]
        public void CanHandleWhiteSpace()
        {
            var actual = DiacriticsCommand.DiacriticsLogic(" ", ToggleMode.Apply);

            Assert.AreEqual(" ", actual);
        }

        [TestMethod]
        public void CanHandleToggleModeNotSet()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("Something", ToggleMode.NotSet);

            Assert.AreEqual("Something", actual);
        }

        [TestMethod]
        public void CanHandleWhenFirstCombiningDiacriticIsNotOneUsedHere()
        {
            var actual = DiacriticsCommand.HasAddedDiacritics("1\u033E\u0347");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void CanHandleWhenSecondCombiningDiacriticIsNotOneUsedHere()
        {
            var actual = DiacriticsCommand.HasAddedDiacritics("1\u033F\u033E");

            Assert.IsFalse(actual);
        }

        [TestMethod]
        public void RemoveWhenNotAllLettersHaveDiacticsAdded()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("s͇̿o͇̿m͇̿e͇̿ t͇̿e͇̿x͇̿t͇̿ X m͇̿o͇̿r͇̿e͇̿ t͇̿e͇̿x͇̿t͇̿", ToggleMode.Reverse);

            Assert.AreEqual("some text X more text", actual);
        }

        [TestMethod]
        public void RemoveWhenSomeLettersHaveOtherDiacriticsAdded()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("s͇̿o͇̿m͇̿e͇̿ X a\u0306", ToggleMode.Reverse);

            Assert.AreEqual("some X a\u0306", actual);
        }

        [TestMethod]
        public void RemoveWhenSomeLettersHaveMultipleOtherDiacriticsAdded()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("s͇̿o͇̿m͇̿e͇̿ X a\u0306\u033E", ToggleMode.Reverse);

            Assert.AreEqual("some X a\u0306\u033E", actual);
        }

        [TestMethod]
        public void HandleRemovingDiacriticsWhenAddedAreMixedWithOthers_FirstAdditionsInvalid()
        {
            // a - extra in third addition
            // b - extra in second addition
            // c - extra in first position - should have removals
            // d - 2 extras in first & second position - should have removals
            // e - no extras - all should be removed
            var actual = DiacriticsCommand.RemoveDiacritics(
                "a\u0306\u032E\u033Eb\u0306\u033E\u032Ec\u033E\u0306\u032Ed\u033E\u033F\u0306\u032Ee\u0306\u032E");

            // The above would fail if calling DiacriticsLogic with ToggleMode.Reverse as the
            // combining characters added to the first letter 'a' don't look right.
            // See next test for variation on this theme.
            Assert.AreEqual(
                "a\u0306\u032E\u033Eb\u0306\u033E\u032Ec\u033Ed\u033E\u033Fe", actual);
        }

        [TestMethod]
        public void HandleRemovingDiacriticsWhenAddedAreMixedWithOthers_FirstAdditionsInvalid_SoNoAdditionDetectedAndNothingRemoved()
        {
            // Note. The scenario tested here should never happen in reality as it's using the wrong toggle mode for the input string.
            //// a - extra in third addition
            //// b - extra in second addition
            //// c - extra in first position - should have removals
            //// d - 2 extras in first & second position - should have removals
            //// e - no extras - all should be removed
            var testStr = "a\u0306\u032E\u033Eb\u0306\u033E\u032Ec\u033E\u0306\u032Ed\u033E\u033F\u0306\u032Ee\u0306\u032E";

            var actual = DiacriticsCommand.DiacriticsLogic(testStr, ToggleMode.Reverse);

            Assert.AreEqual(testStr, actual);
        }

        [TestMethod]
        public void HandleRemovingDiacriticsWhenAddedAreMixedWithOthers_FirstAdditionsValid_SoHaveRemovals()
        {
            //// z - no extras
            //// a - extra in third addition
            //// b - extra in second addition
            //// c - extra in first position - should have removals
            //// d - 2 extras in first & second position - should have removals
            //// e - no extras - all should be removed
            var testStr = "z\u0306\u032Ea\u0306\u032E\u033Eb\u0306\u033E\u032Ec\u033E\u0306\u032Ed\u033E\u033F\u0306\u032Ee\u0306\u032E";

            var actual = DiacriticsCommand.DiacriticsLogic(testStr, ToggleMode.Reverse);

            Assert.AreEqual("za\u0306\u032E\u033Eb\u0306\u033E\u032Ec\u033Ed\u033E\u033Fe", actual);
        }
    }
}
