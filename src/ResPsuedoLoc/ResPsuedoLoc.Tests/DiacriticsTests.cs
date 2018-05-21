// <copyright file="DiacriticsTests.cs" company="Matt Lacey Ltd.">
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
        public void AddToStringOfLength1()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a");

            Assert.AreEqual("a\u033F\u0347", actual);
        }

        [TestMethod]
        public void AddToStringOfLength2()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("ab");

            Assert.AreEqual("a\u0306\u032Eb\u0306\u032E", actual);
        }

        [TestMethod]
        public void AddToStringOfLength3()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abc");

            Assert.AreEqual("a\u0346\u033Ab\u0346\u033Ac\u0346\u033A", actual);
        }

        [TestMethod]
        public void AddToStringOfLength4()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcd");

            Assert.AreEqual("a\u030A\u0325b\u030A\u0325c\u030A\u0325d\u030A\u0325", actual);
        }

        [TestMethod]
        public void AddToStringOfLength5()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcde");

            Assert.AreEqual("a\u0302\u032Db\u0302\u032Dc\u0302\u032Dd\u0302\u032De\u0302\u032D", actual);
        }

        [TestMethod]
        public void AddToStringOfLength6()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdef");

            Assert.AreEqual("a\u0300\u0316b\u0300\u0316c\u0300\u0316d\u0300\u0316e\u0300\u0316f\u0300\u0316", actual);
        }

        [TestMethod]
        public void AddToStringOfLength7()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdefg");

            Assert.AreEqual("a\u0301\u0317b\u0301\u0317c\u0301\u0317d\u0301\u0317e\u0301\u0317f\u0301\u0317g\u0301\u0317", actual);
        }

        [TestMethod]
        public void AddToStringOfLength8()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdefgh");

            Assert.AreEqual("a\u0309\u0321b\u0309\u0321c\u0309\u0321d\u0309\u0321e\u0309\u0321f\u0309\u0321g\u0309\u0321h\u0309\u0321", actual);
        }

        [TestMethod]
        public void AddToStringOfLength9()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdefghi");

            Assert.AreEqual("a\u0308\u0324b\u0308\u0324c\u0308\u0324d\u0308\u0324e\u0308\u0324f\u0308\u0324g\u0308\u0324h\u0308\u0324i\u0308\u0324", actual);
        }

        [TestMethod]
        public void AddToStringOfLength10()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("abcdefghij");

            Assert.AreEqual("a\u033F\u0347b\u033F\u0347c\u033F\u0347d\u033F\u0347e\u033F\u0347f\u033F\u0347g\u033F\u0347h\u033F\u0347i\u033F\u0347j\u033F\u0347", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength1()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u033F\u0347");

            Assert.AreEqual("a", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength2()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0306\u032Eb\u0306\u032E");

            Assert.AreEqual("ab", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength3()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0346\u033Ab\u0346\u033Ac\u0346\u033A");

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength4()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u030A\u0325b\u030A\u0325c\u030A\u0325d\u030A\u0325");

            Assert.AreEqual("abcd", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength5()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0302\u032Db\u0302\u032Dc\u0302\u032Dd\u0302\u032De\u0302\u032D");

            Assert.AreEqual("abcde", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength6()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0300\u0316b\u0300\u0316c\u0300\u0316d\u0300\u0316e\u0300\u0316f\u0300\u0316");

            Assert.AreEqual("abcdef", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength7()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0301\u0317b\u0301\u0317c\u0301\u0317d\u0301\u0317e\u0301\u0317f\u0301\u0317g\u0301\u0317");

            Assert.AreEqual("abcdefg", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength8()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0309\u0321b\u0309\u0321c\u0309\u0321d\u0309\u0321e\u0309\u0321f\u0309\u0321g\u0309\u0321h\u0309\u0321");

            Assert.AreEqual("abcdefgh", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength9()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0308\u0324b\u0308\u0324c\u0308\u0324d\u0308\u0324e\u0308\u0324f\u0308\u0324g\u0308\u0324h\u0308\u0324i\u0308\u0324");

            Assert.AreEqual("abcdefghi", actual);
        }

        [TestMethod]
        public void RemoveInStringOfLength10()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u033F\u0347b\u033F\u0347c\u033F\u0347d\u033F\u0347e\u033F\u0347f\u033F\u0347g\u033F\u0347h\u033F\u0347i\u033F\u0347j\u033F\u0347");

            Assert.AreEqual("abcdefghij", actual);
        }

        [TestMethod]
        public void AddToStringSkipsSpaces()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("ab de");

            Assert.AreEqual("a\u0302\u032Db\u0302\u032D d\u0302\u032De\u0302\u032D", actual);
        }

        [TestMethod]
        public void RemovingFromStringSkipsSpaces()
        {
            var actual = DiacriticsCommand.DiacriticsLogic("a\u0302\u032Db\u0302\u032D d\u0302\u032De\u0302\u032D");

            Assert.AreEqual("ab de", actual);
        }
    }
}
