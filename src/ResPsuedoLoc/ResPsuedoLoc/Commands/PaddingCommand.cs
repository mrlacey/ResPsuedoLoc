// <copyright file="PaddingCommand.cs" company="Matt Lacey Ltd.">
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
    public sealed class PaddingCommand : ReversableCommand
    {
        public const int CommandId = 4126;
#pragma warning disable SA1401 // Fields must be private
        public static char Separator = '-';
        public static string SeparatorStr = Separator.ToString();
#pragma warning restore SA1401 // Fields must be private

        private PaddingCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            commandService.AddCommand(menuItem);
        }

        private PaddingCommand()
            : base()
        {
            // For testing use only
        }

        public static PaddingCommand Instance { get; private set; }

        public static PaddingCommand CreateForTesting()
        {
            return new PaddingCommand();
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new PaddingCommand(package, commandService);
        }

        public static bool IsPadded(string input)
        {
            if (input.Length < 3)
            {
                return false;
            }
            else
            {
                if (SurroundCommand.IsSurrounded(input))
                {
                    input = SurroundCommand.RemoveSurrounds(input);
                }

                var countOfLetters = input.Count(char.IsLetter);
                var countOfSeparators = input.Count(i => i == Separator);

                return countOfSeparators >= (countOfLetters / 2);
            }
        }

        public static string PaddingLogic(string input, ToggleMode toggleMode)
        {
            if (input.Length < 2 || toggleMode == ToggleMode.NotSet)
            {
                return input;
            }
            else if (input.Length == 2)
            {
                if (toggleMode == ToggleMode.Apply && char.IsLetter(input[0]) && char.IsLetter(input[1]))
                {
                    return input.Insert(1, SeparatorStr);
                }
                else
                {
                    return input;
                }
            }
            else
            {
                if (toggleMode == ToggleMode.Apply)
                {
                    if (IsPadded(input))
                    {
                        return input;
                    }
                    else
                    {
                        return AddPadding(input);
                    }
                }
                else
                {
                    if (IsPadded(input))
                    {
                        return RemovePadding(input);
                    }
                    else
                    {
                        return input;
                    }
                }
            }
        }

        public static string AddPadding(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToAdjust = input;

            if (surrounded)
            {
                stringToAdjust = SurroundCommand.RemoveSurrounds(input);
            }

            var result = new StringBuilder();

            if (stringToAdjust.Length < 2)
            {
                result.Append(stringToAdjust);
            }
            else if (stringToAdjust.Length == 2)
            {
                if (char.IsLetter(stringToAdjust[0]) && char.IsLetter(stringToAdjust[1]))
                {
                    result.Append(stringToAdjust[0]);
                    result.Append(Separator);
                    result.Append(stringToAdjust[1]);
                }
                else
                {
                    result.Append(stringToAdjust);
                }
            }
            else
            {
                var words = stringToAdjust.Split(' ');

                foreach (var word in words)
                {
                    if (word.Length < 2)
                    {
                        result.Append(word);
                    }
                    else
                    {
                        var paddedWord = new StringBuilder();

                        foreach (var character in word.GetGraphemeClusters())
                        {
                            paddedWord.Append($"{character}{Separator}");
                        }

                        result.Append(paddedWord.ToString().TrimEnd(Separator));
                    }

                    result.Append(' ');
                }

                result.Remove(result.Length - 1, 1);
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

        public static string RemovePadding(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToAdjust = input;

            if (surrounded)
            {
                stringToAdjust = SurroundCommand.RemoveSurrounds(input);
            }

            var result = new StringBuilder();

            if (stringToAdjust.Length < 3)
            {
                result.Append(input);
            }
            else
            {
                var words = stringToAdjust.Split(' ');

                foreach (var word in words)
                {
                    if (word.Length < 2)
                    {
                        result.Append(word);
                    }
                    else
                    {
                        var tempWord = word.Replace("---", " ");

                        tempWord = tempWord.Replace(SeparatorStr, string.Empty);

                        result.Append(tempWord.Replace(" ", SeparatorStr));
                    }

                    result.Append(' ');
                }

                result.Remove(result.Length - 1, 1);
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

        public string PaddingLogic(string input)
        {
            if (this.Mode == ToggleMode.NotSet)
            {
                this.Mode = this.GetToggleMode(input);
            }

            return PaddingCommand.PaddingLogic(input, this.Mode);
        }

        public override List<string> TestActingOnMultipleStrings(List<string> inputs)
        {
            var result = new List<string>();

            foreach (var input in inputs)
            {
                result.Add(this.PaddingLogic(input));
            }

            return result;
        }

        internal new ToggleMode GetToggleMode(string input)
        {
            return PaddingCommand.IsPadded(input) ? ToggleMode.Reverse : ToggleMode.Apply;
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.Mode = ToggleMode.NotSet;

            this.ForEachStringResourceEntry(this.PaddingLogic);
        }
    }
}
