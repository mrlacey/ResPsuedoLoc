// <copyright file="AssertExtensions.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ResPsuedoLoc.Tests
{
    public static class AssertExtensions
    {
       public static void StringListsAreEqual(this Assert assert, List<string> expected, List<string> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            for (var i = 0; i < actual.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i], $"{Environment.NewLine}Values at position {i} are not equal.");
            }
        }
    }
}
