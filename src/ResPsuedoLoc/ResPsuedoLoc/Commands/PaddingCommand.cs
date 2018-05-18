﻿using System;
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
        private static char separator = '-';
        public static string SeparatorStr = separator.ToString();

        private PaddingCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            menuItem.BeforeQueryStatus += this.MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        public static PaddingCommand Instance
        {
            get;
            private set;
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Verify the current thread is the UI thread - the call to AddCommand in SurroundCommand's constructor requires
            // the UI thread.
            ThreadHelper.ThrowIfNotOnUIThread();

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new PaddingCommand(package, commandService);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            ForEachStringResourceEntry(PaddingLogic);
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
                var countOfSeparators = input.Count(i => i == separator);

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
                        result.Append(separator);
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
                                paddedWord.Append($"{character}{separator}");
                            }

                            result.Append(paddedWord.ToString().TrimEnd(separator));
                        }
                        else
                        {
                            var tempWord = word.Replace("---", " ");

                            tempWord = tempWord.Replace(SeparatorStr, string.Empty);

                            result.Append(tempWord.Replace(" ", SeparatorStr));
                        }

                        result.Append(' ');
                    }

                    result.Remove(result.Length -1, 1);
                }

                return result.ToString();
            }
        }
    }
}
