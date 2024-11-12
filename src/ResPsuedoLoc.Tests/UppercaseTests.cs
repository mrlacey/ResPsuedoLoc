// <copyright file="UppercaseTests.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class UppercaseTests
    {
        [TestMethod]
        public void SimpleFunctionalityWorksOk()
        {
            var source = "lowercase";

            var actual = UppercaseCommand.UppercaseLogic(source);

            Assert.AreEqual("LOWERCASE", actual);
        }

        [TestMethod]
        public void DoesNothingIfAllAlreadyUppercase()
        {
            var source = "UPPERCASE";

            var actual = UppercaseCommand.UppercaseLogic(source);

            Assert.AreEqual("UPPERCASE", actual);
        }

        [TestMethod]
        public void IgnoresSpacesAtEndsAndMiddle()
        {
            var source = " lower case ";

            var actual = UppercaseCommand.UppercaseLogic(source);

            Assert.AreEqual(" LOWER CASE ", actual);
        }

        [TestMethod]
        public void HandlesEmptyString()
        {
            var actual = UppercaseCommand.UppercaseLogic(string.Empty);

            Assert.AreEqual(string.Empty, actual);
        }
    }
}
