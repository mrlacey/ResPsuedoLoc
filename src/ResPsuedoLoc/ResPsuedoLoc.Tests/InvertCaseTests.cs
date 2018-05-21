// <copyright file="InvertCaseTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class InvertCaseTests
    {
        [TestMethod]
        public void AllUpper()
        {
            var actual = InvertCaseCommand.InvertCaseLogic("ABC");

            Assert.AreEqual("abc", actual);
        }

        [TestMethod]
        public void AllLower()
        {
            var actual = InvertCaseCommand.InvertCaseLogic("abc");

            Assert.AreEqual("ABC", actual);
        }

        [TestMethod]
        public void PascalCase()
        {
            var actual = InvertCaseCommand.InvertCaseLogic("PascalCase");

            Assert.AreEqual("pASCALcASE", actual);
        }

        [TestMethod]
        public void CascalCase()
        {
            var actual = InvertCaseCommand.InvertCaseLogic("camelCase");

            Assert.AreEqual("CAMELcASE", actual);
        }

        [TestMethod]
        public void MixedCaseWithSpaces()
        {
            var actual = InvertCaseCommand.InvertCaseLogic("This Is A Test");

            Assert.AreEqual("tHIS iS a tEST", actual);
        }
    }
}
