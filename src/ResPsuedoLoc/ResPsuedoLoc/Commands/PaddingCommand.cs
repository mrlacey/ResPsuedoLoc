// <copyright file="PaddingCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class PaddingCommand : BaseCommand
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

        public static PaddingCommand Instance
        {
            get;
            private set;
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new PaddingCommand(package, commandService);
        }

        public static string PaddingLogic(string input)
        {
            if (input.Length < 2)
            {
                return input;
            }
            else if (input.Length == 2)
            {
                if (char.IsLetter(input[0]) && char.IsLetter(input[1]))
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
                var result = new StringBuilder();

                var countOfLetters = input.Count(i => char.IsLetter(i));
                var countOfSeparators = input.Count(i => i == Separator);

                var adding = countOfSeparators < (countOfLetters / 2);

                if (input.Length < 2)
                {
                    result.Append(input);
                }
                else if (input.Length == 2)
                {
                    if (char.IsLetter(input[0]) && char.IsLetter(input[1]))
                    {
                        result.Append(input[0]);
                        result.Append(Separator);
                        result.Append(input[1]);
                    }
                    else
                    {
                        result.Append(input);
                    }
                }
                else
                {
                    var words = input.Split(' ');

                    foreach (var word in words)
                    {
                        if (word.Length < 2)
                        {
                            result.Append(word);
                        }
                        else if (adding)
                        {
                            var paddedWord = new StringBuilder();

                            foreach (var character in word.GetGraphemeClusters())
                            {
                                paddedWord.Append($"{character}{Separator}");
                            }

                            result.Append(paddedWord.ToString().TrimEnd(Separator));
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

                return result.ToString();
            }
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.ForEachStringResourceEntry(PaddingLogic);
        }
    }
}
