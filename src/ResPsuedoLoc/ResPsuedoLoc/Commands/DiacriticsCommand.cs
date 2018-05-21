// <copyright file="DiacriticsCommand.cs" company="Matt Lacey Ltd.">
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
    public sealed class DiacriticsCommand : BaseCommand
    {
        public const int CommandId = 4124;

        private DiacriticsCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            menuItem.BeforeQueryStatus += this.MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        public static DiacriticsCommand Instance
        {
            get;
            private set;
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Verify the current thread is the UI thread - the call to AddCommand in SurroundCommand's constructor requires
            // the UI thread.
            ThreadHelper.ThrowIfNotOnUIThread();

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new DiacriticsCommand(package, commandService);
        }

        public static string DiacriticsLogic(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToReverse = input;

            if (surrounded)
            {
                stringToReverse = SurroundCommand.RemoveSurrounds(input);
            }

            var letters = stringToReverse.GetGraphemeClusters().ToList();

            var result = new StringBuilder();

            // umlaut, equals, breve, bridge, ring, circumflex, grave, acute, hook
            var topOptions = new[] { '\u0308', '\u033F', '\u0306', '\u0346', '\u030A', '\u0302', '\u0300', '\u0301', '\u0309' };
            var bottomOptions = new[] { '\u0324', '\u0347', '\u032E', '\u033A', '\u0325', '\u032D', '\u0316', '\u0317', '\u0321' };

            var optionsIndex = letters.Count % topOptions.Length;

            if (letters.Count > 0)
            {
                var adding = true;

                if (letters[0].Length > 2
                 && letters[0][letters[0].Length - 2] == topOptions[optionsIndex]
                 && letters[0][letters[0].Length - 1] == bottomOptions[optionsIndex])
                {
                    adding = false;
                }

                foreach (var letter in letters)
                {
                    // It should never be null, but want to check for whitespace
                    if (string.IsNullOrWhiteSpace(letter) || letter == PaddingCommand.SeparatorStr)
                    {
                        result.Append(letter);
                    }
                    else
                    {
                        if (adding)
                        {
                            result.Append(letter);
                            result.Append(topOptions[optionsIndex]);
                            result.Append(bottomOptions[optionsIndex]);
                        }
                        else
                        {
                            result.Append(letter.Substring(0, letter.Length - 2));
                        }
                    }
                }
            }

            if (surrounded)
            {
                return SurroundCommand.SurroundLogic(result.ToString());
            }
            else
            {
                return result.ToString();
            }
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.ForEachStringResourceEntry(DiacriticsLogic);
        }
    }
}
