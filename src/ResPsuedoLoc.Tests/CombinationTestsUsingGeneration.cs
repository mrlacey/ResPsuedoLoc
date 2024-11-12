// <copyright file="CombinationTestsUsingGeneration.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ResPsuedoLoc.Commands;

namespace ResPsuedoLoc.Tests
{
    [TestClass]
    public class CombinationTestsUsingGeneration
    {
        public static string[] GetAllCommands()
        {
            return new[]
            {
                nameof(DiacriticsCommand),
                nameof(DoubleCommand),
                nameof(InvertCaseCommand),
                nameof(PaddingCommand),
                nameof(ReverseCommand),
                nameof(SurroundCommand),
            };
        }

        public static IEnumerable<object[]> GetAllCommandPermutations()
        {
            return GetCommandListOfLength(6);
        }

        public static IEnumerable<object[]> GetAllCommandPermutations_Any5()
        {
            return GetCommandListOfLength(5);
        }

        public static IEnumerable<object[]> GetAllCommandPermutations_Any4()
        {
            return GetCommandListOfLength(4);
        }

        public static IEnumerable<object[]> GetAllCommandPermutations_Any3()
        {
            return GetCommandListOfLength(3);
        }

        public static IEnumerable<object[]> GetAllCommandPermutations_Any2()
        {
            return GetCommandListOfLength(2);
        }

        public static IEnumerable<object[]> GetCommandListOfLength(int length)
        {
            var commands = GetAllCommands();

            var permutations = GetPermutations(commands, length).ToList();

            foreach (var permutation in permutations)
            {
                yield return new object[] { permutation };
            }
        }

        public static IEnumerable<object[]> GetAllAddAndRemoveCommandPermutations_Any4()
        {
            var commands = GetAllCommands();

            // 4 should be enough to identify any issues of order combinations having unexpected effects.
            // Doing more than this produces many more combinations and requires a massive amount of memory,
            // such that it can lock up VS while trying to load the results.
            var addPermutations = GetPermutations(commands, 4).ToList();

            foreach (var addPermutation in addPermutations)
            {
                var removePermutations = GetPermutations(addPermutation).ToList();

                foreach (var removePermutation in removePermutations)
                {
                    yield return new object[] { addPermutation, removePermutation };
                }
            }
        }

        public static string Apply(string input, string command)
        {
            switch (command)
            {
                case nameof(DiacriticsCommand):
                    return DiacriticsCommand.DiacriticsLogic(input, ToggleMode.Apply);
                case nameof(DoubleCommand):
                    return DoubleCommand.DoubleLogic(input, ToggleMode.Apply);
                case nameof(InvertCaseCommand):
                    return InvertCaseCommand.InvertCaseLogic(input);
                case nameof(PaddingCommand):
                    return PaddingCommand.PaddingLogic(input, ToggleMode.Apply);
                case nameof(ReverseCommand):
                    return ReverseCommand.ReverseLogic(input);
                case nameof(SurroundCommand):
                    return SurroundCommand.SurroundLogic(input, ToggleMode.Apply);
                default:
                    return input;
            }
        }

        public static string Reverse(string input, string command)
        {
            switch (command)
            {
                case nameof(DiacriticsCommand):
                    return DiacriticsCommand.DiacriticsLogic(input, ToggleMode.Reverse);
                case nameof(DoubleCommand):
                    return DoubleCommand.DoubleLogic(input, ToggleMode.Reverse);
                case nameof(InvertCaseCommand):
                    return InvertCaseCommand.InvertCaseLogic(input);
                case nameof(PaddingCommand):
                    return PaddingCommand.PaddingLogic(input, ToggleMode.Reverse);
                case nameof(ReverseCommand):
                    return ReverseCommand.ReverseLogic(input);
                case nameof(SurroundCommand):
                    return SurroundCommand.SurroundLogic(input, ToggleMode.Reverse);
                default:
                    return input;
            }
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations), DynamicDataSourceType.Method)]
        public void AddAndRemoveInSameOrder_AllCombinations_All6(IEnumerable<string> permutation)
        {
            this.RemoveInOrderAdded(permutation);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations_Any5), DynamicDataSourceType.Method)]
        public void AddAndRemoveInSameOrder_AllCombinations_Only5(IEnumerable<string> permutation)
        {
            this.RemoveInOrderAdded(permutation);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations_Any4), DynamicDataSourceType.Method)]
        public void AddAndRemoveInSameOrder_AllCombinations_Only4(IEnumerable<string> permutation)
        {
            this.RemoveInOrderAdded(permutation);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations_Any3), DynamicDataSourceType.Method)]
        public void AddAndRemoveInSameOrder_AllCombinations_Only3(IEnumerable<string> permutation)
        {
            this.RemoveInOrderAdded(permutation);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations_Any2), DynamicDataSourceType.Method)]
        public void AddAndRemoveInSameOrder_AllCombinations_Only2(IEnumerable<string> permutation)
        {
            this.RemoveInOrderAdded(permutation);
        }

        public void RemoveInOrderAdded(IEnumerable<string> permutation)
        {
            var origin = "Original String";

            var modified = origin;

            var orderedCommands = permutation.ToList();

            foreach (var orderedCommand in orderedCommands)
            {
                modified = Apply(modified, orderedCommand);
            }

            foreach (var orderedCommand in orderedCommands)
            {
                modified = Reverse(modified, orderedCommand);
            }

            Assert.AreEqual(origin, modified, $"For permutation: {string.Join(", ", orderedCommands)}");
        }

        public void RemoveInReverseOrderToAdded(IEnumerable<string> permutation)
        {
            var origin = "Original String";

            var modified = origin;

            var orderedCommands = permutation.ToList();

            foreach (var orderedCommand in orderedCommands)
            {
                modified = Apply(modified, orderedCommand);
            }

            var reverseOrderedCommands = new string[orderedCommands.Count];
            orderedCommands.CopyTo(reverseOrderedCommands);
            var reverseOrder = reverseOrderedCommands.Reverse();

            foreach (var orderedCommand in reverseOrder)
            {
                modified = Reverse(modified, orderedCommand);
            }

            Assert.AreEqual(origin, modified, $"For permutation: {string.Join(", ", orderedCommands)}, {string.Join(", ", reverseOrder)}");
        }

        public void AddAndRemoveInSpecifiedOrders(IEnumerable<string> addOrder, IEnumerable<string> removeOrder)
        {
            var origin = "Original String";

            var modified = origin;

            var orderedCommands = addOrder.ToList();

            foreach (var orderedCommand in orderedCommands)
            {
                modified = Apply(modified, orderedCommand);
            }

            var reverseOrder = removeOrder.ToList();

            foreach (var orderedCommand in reverseOrder)
            {
                modified = Reverse(modified, orderedCommand);
            }

            Assert.AreEqual(origin, modified, $"For permutation: {string.Join(", ", orderedCommands)}, {string.Join(", ", reverseOrder)}");
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations), DynamicDataSourceType.Method)]
        public void AddAndRemoveInReverseOrder_AllCombinations_All6(IEnumerable<string> permutation)
        {
            this.RemoveInReverseOrderToAdded(permutation);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations_Any5), DynamicDataSourceType.Method)]
        public void AddAndRemoveInReverseOrder_AllCombinations_Only5(IEnumerable<string> permutation)
        {
            this.RemoveInReverseOrderToAdded(permutation);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations_Any4), DynamicDataSourceType.Method)]
        public void AddAndRemoveInReverseOrder_AllCombinations_Only4(IEnumerable<string> permutation)
        {
            this.RemoveInReverseOrderToAdded(permutation);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations_Any3), DynamicDataSourceType.Method)]
        public void AddAndRemoveInReverseOrder_AllCombinations_Only3(IEnumerable<string> permutation)
        {
            this.RemoveInReverseOrderToAdded(permutation);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllCommandPermutations_Any2), DynamicDataSourceType.Method)]
        public void AddAndRemoveInReverseOrder_AllCombinations_Only2(IEnumerable<string> permutation)
        {
            this.RemoveInReverseOrderToAdded(permutation);
        }

        [DataTestMethod]
        [DynamicData(nameof(GetAllAddAndRemoveCommandPermutations_Any4), DynamicDataSourceType.Method)]
        public void AddAndRemoveInAllOrders(IEnumerable<string> addOrder, IEnumerable<string> removeOrder)
        {
            // This does 8640 combinations of input and includes overlap with some more targeted tests but all still useful
            this.AddAndRemoveInSpecifiedOrders(addOrder, removeOrder);
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list)
        {
            return GetPermutations(list, list.Count());
        }

        private static IEnumerable<IEnumerable<T>> GetPermutations<T>(IEnumerable<T> list, int length)
        {
            if (length == 1)
            {
                return list.Select(t => new T[] { t });
            }

            return GetPermutations(list, length - 1)
                  .SelectMany(t => list.Where(o => !t.Contains(o)), (t1, t2) => t1.Concat(new T[] { t2 }));
        }
    }
}
