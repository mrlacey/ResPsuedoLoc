// <copyright file="DoubleCommand.cs" company="Matt Lacey Ltd.">
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
    public sealed class DoubleCommand : ReversableCommand
    {
        public const int CommandId = 4129;

        private DoubleCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            menuItem.BeforeQueryStatus += this.MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        private DoubleCommand()
            : base()
        {
            // For testing use only
        }

        public static DoubleCommand Instance { get; private set; }

        public static DoubleCommand CreateForTesting()
        {
            return new DoubleCommand();
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new DoubleCommand(package, commandService);
        }

        public static string DoubleLogic(string input, ToggleMode toggleMode)
        {
            if (string.IsNullOrEmpty(input) || toggleMode == ToggleMode.NotSet)
            {
                return input;
            }

            if (toggleMode == ToggleMode.Apply)
            {
                if (IsDoubled(input))
                {
                    return input;
                }
                else
                {
                    return Double(input);
                }
            }
            else
            {
                if (IsDoubled(input))
                {
                    return RemoveDoubling(input);
                }
                else
                {
                    return input;
                }
            }
        }

        public string DoubleLogic(string input)
        {
            if (this.Mode == ToggleMode.NotSet)
            {
                this.Mode = this.GetToggleMode(input);
            }

            return DoubleLogic(input, this.Mode);
        }

        public override List<string> TestActingOnMultipleStrings(List<string> inputs)
        {
            var result = new List<string>();

            foreach (var input in inputs)
            {
                result.Add(this.DoubleLogic(input));
            }

            return result;
        }

        internal static bool IsDoubled(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToAdjust = input;

            if (surrounded)
            {
                stringToAdjust = SurroundCommand.RemoveSurrounds(input);
            }

            var letters = stringToAdjust.GetGraphemeClusters().ToList();

            var justLetters = new List<string>();

            foreach (var letter in letters)
            {
                if (char.IsLetter(letter[0]))
                {
                    justLetters.Add(letter);
                }
            }

            for (var i = 1; i < justLetters.Count; i += 2)
            {
                if (justLetters[i] != justLetters[i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        internal static string RemoveDoubling(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToAdjust = input;

            if (surrounded)
            {
                stringToAdjust = SurroundCommand.RemoveSurrounds(input);
            }

            var padded = PaddingCommand.IsPadded(stringToAdjust);

            if (padded)
            {
                stringToAdjust = PaddingCommand.PaddingLogic(stringToAdjust, ToggleMode.Reverse);
            }

            var letters = stringToAdjust.GetGraphemeClusters().ToList();

            var result = new StringBuilder();

            var i = 0;

            while (i < letters.Count)
            {
                result.Append(letters[i]);

                if (char.IsLetter(letters[i][0]))
                {
                    i += 2;
                }
                else
                {
                    ++i;
                }
            }

            var resultString = result.ToString();

            if (padded)
            {
                resultString = PaddingCommand.PaddingLogic(resultString, ToggleMode.Apply);
            }

            if (surrounded)
            {
                resultString = SurroundCommand.SurroundLogic(resultString, ToggleMode.Apply);
            }

            return resultString;
        }

        internal static string Double(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToAdjust = input;

            if (surrounded)
            {
                stringToAdjust = SurroundCommand.RemoveSurrounds(input);
            }

            var padded = PaddingCommand.IsPadded(stringToAdjust);

            if (padded)
            {
                stringToAdjust = PaddingCommand.RemovePadding(stringToAdjust);
            }

            var letters = stringToAdjust.GetGraphemeClusters().ToList();

            var result = new StringBuilder();

            foreach (var letter in letters)
            {
                if (char.IsLetter(letter[0]))
                {
                    result.Append(letter);
                }

                result.Append(letter);
            }

            var finalResult = result.ToString();

            if (padded)
            {
                finalResult = PaddingCommand.AddPadding(finalResult);
            }

            if (surrounded)
            {
                finalResult = SurroundCommand.SurroundLogic(finalResult, ToggleMode.Apply);
            }

            return finalResult;
        }

        internal new ToggleMode GetToggleMode(string input)
        {
            return IsDoubled(input) ? ToggleMode.Reverse : ToggleMode.Apply;
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.Mode = ToggleMode.NotSet;

            this.ForEachStringResourceEntry(this.DoubleLogic);
        }
    }
}
