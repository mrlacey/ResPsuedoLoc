// <copyright file="DiacriticsCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class DiacriticsCommand : ReversableCommand
    {
        public const int CommandId = 4124;

        // umlaut, equals, breve, bridge, ring, circumflex, grave, acute, hook
        private static readonly char[] TopOptions = new[] { '\u0308', '\u033F', '\u0306', '\u0346', '\u030A', '\u0302', '\u0300', '\u0301', '\u0309' };
        private static readonly char[] BottomOptions = new[] { '\u0324', '\u0347', '\u032E', '\u033A', '\u0325', '\u032D', '\u0316', '\u0317', '\u0321' };

        private DiacriticsCommand(PsuedoLocPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            menuItem.BeforeQueryStatus += this.MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        private DiacriticsCommand()
            : base()
        {
            // For testing use only
        }

        public static DiacriticsCommand Instance { get; private set; }

        public static DiacriticsCommand CreateForTesting()
        {
            return new DiacriticsCommand();
        }

        public static async Task InitializeAsync(PsuedoLocPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new DiacriticsCommand(package, commandService);
        }

        public static string DiacriticsLogic(string input, ToggleMode toggleMode)
        {
            if (toggleMode == ToggleMode.NotSet || string.IsNullOrWhiteSpace(input))
            {
                return input;
            }

            if (toggleMode == ToggleMode.Apply)
            {
                if (HasAddedDiacritics(input))
                {
                    return input;
                }
                else
                {
                    return AddDiacritics(input);
                }
            }
            else
            {
                if (HasAddedDiacritics(input))
                {
                    return RemoveDiacritics(input);
                }
                else
                {
                    return input;
                }
            }
        }

        public static bool HasAddedDiacritics(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToAdjust = input;

            if (surrounded)
            {
                stringToAdjust = SurroundCommand.RemoveSurrounds(input);
            }

            var textElements = stringToAdjust.GetGraphemeClusters().ToList();

            if (textElements.Count > 0)
            {
                for (var i = 0; i < textElements.Count; i++)
                {
                    // Find the first text element with added diacritics
                    if (textElements[i].Length > 2)
                    {
                        return HasSubstringGotAddedDiacritics(textElements[i]);
                    }
                }
            }

            return false;
        }

        public static bool HasSubstringGotAddedDiacritics(string graphemeCluster)
        {
            if (graphemeCluster.Length <= 2)
            {
                return false;
            }

            // Check that the same above and below diacritics are added as we would
            // Don't check for the expected diacritics based on string length as combinations of actions can change this
            var topAddition = graphemeCluster[graphemeCluster.Length - 2];
            var bottomAddition = graphemeCluster[graphemeCluster.Length - 1];

            var topIndex = TopOptions.ToList().IndexOf(topAddition);
            var bottomIndex = BottomOptions.ToList().IndexOf(bottomAddition);

            return topIndex >= 0 && topIndex == bottomIndex;
        }

        public static string AddDiacritics(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToAdjust = input;

            if (surrounded)
            {
                stringToAdjust = SurroundCommand.RemoveSurrounds(input);
            }

            var letters = stringToAdjust.GetGraphemeClusters().ToList();

            var optionsIndex = letters.Count % TopOptions.Length;

            var result = new StringBuilder();

            foreach (var letter in letters)
            {
                // It should never be null, but want to check for whitespace
                if (string.IsNullOrWhiteSpace(letter) || letter == PaddingCommand.SeparatorStr)
                {
                    result.Append(letter);
                }
                else
                {
                    result.Append(letter);
                    result.Append(TopOptions[optionsIndex]);
                    result.Append(BottomOptions[optionsIndex]);
                }
            }

            if (surrounded)
            {
                return SurroundCommand.SurroundLogic(result.ToString(), ToggleMode.Apply);
            }
            else
            {
                return result.ToString();
            }
        }

        public static string RemoveDiacritics(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToAdjust = input;

            if (surrounded)
            {
                stringToAdjust = SurroundCommand.RemoveSurrounds(input);
            }

            var letters = stringToAdjust.GetGraphemeClusters().ToList();

            var result = new StringBuilder();

            foreach (var letter in letters)
            {
                // It should never be null, but want to check for whitespace
                if (string.IsNullOrWhiteSpace(letter) || letter == PaddingCommand.SeparatorStr)
                {
                    result.Append(letter);
                }
                else
                {
                    if (HasSubstringGotAddedDiacritics(letter))
                    {
                        result.Append(letter.Substring(0, letter.Length - 2));
                    }
                    else
                    {
                        result.Append(letter);
                    }
                }
            }

            if (surrounded)
            {
                return SurroundCommand.SurroundLogic(result.ToString(), ToggleMode.Apply);
            }
            else
            {
                return result.ToString();
            }
        }

        public string DiacriticsLogic(string input)
        {
            if (this.Mode == ToggleMode.NotSet)
            {
                this.Mode = this.GetToggleMode(input);
            }

            return DiacriticsCommand.DiacriticsLogic(input, this.Mode);
        }

        public override List<string> TestActingOnMultipleStrings(List<string> inputs)
        {
            var result = new List<string>();

            foreach (var input in inputs)
            {
                result.Add(this.DiacriticsLogic(input));
            }

            return result;
        }

        internal new ToggleMode GetToggleMode(string input)
        {
            return HasAddedDiacritics(input) ? ToggleMode.Reverse : ToggleMode.Apply;
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.Mode = ToggleMode.NotSet;

            this.ForEachStringResourceEntry(this.DiacriticsLogic);
        }
    }
}
