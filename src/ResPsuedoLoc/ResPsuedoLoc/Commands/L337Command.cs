// <copyright file="L337Command.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.Design;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class L337Command : BaseCommand
    {
        public const int CommandId = 4132;

        private L337Command(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            commandService.AddCommand(menuItem);
        }

        public static L337Command Instance { get; private set; }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Verify the current thread is the UI thread - the call to AddCommand in DoubleCommand's constructor requires
            // the UI thread.
            ThreadHelper.ThrowIfNotOnUIThread();

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new L337Command(package, commandService);
        }

        public static string L337Logic(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            var result = new StringBuilder();

            foreach (var letter in input)
            {
                if (char.IsLetter(letter))
                {
                    switch (letter.ToString().ToUpperInvariant())
                    {
                        case "I":
                            result.Append('1');
                            break;
                        case "Z":
                            result.Append('2');
                            break;
                        case "E":
                            result.Append('3');
                            break;
                        case "A":
                            result.Append('4');
                            break;
                        case "S":
                            result.Append('5');
                            break;
                        case "G":
                            result.Append('6');
                            break;
                        case "T":
                            result.Append('7');
                            break;
                        case "B":
                            result.Append('8');
                            break;
                        case "Q":
                            result.Append('9');
                            break;
                        case "O":
                            result.Append('0');
                            break;
                        case "C":
                            result.Append('(');
                            break;
                        case "H":
                            result.Append('#');
                            break;
                        case "K":
                            result.Append('{');
                            break;
                        case "X":
                            result.Append('%');
                            break;
                        default:
                            result.Append(letter);
                            break;
                    }
                }
                else
                {
                    result.Append(letter);
                }
            }

            return result.ToString();
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.ForEachStringResourceEntry(L337Logic);
        }
    }
}
