// <copyright file="InvertCaseCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.Design;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class InvertCaseCommand : BaseCommand
    {
        public const int CommandId = 4125;

        private InvertCaseCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            menuItem.BeforeQueryStatus += this.MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        public static InvertCaseCommand Instance
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
            Instance = new InvertCaseCommand(package, commandService);
        }

        public static string InvertCaseLogic(string input)
        {
            var result = new StringBuilder(input.Length);

            foreach (var character in input)
            {
                if (char.IsLetter(character))
                {
                    if (char.IsLower(character))
                    {
                        result.Append(char.ToUpperInvariant(character));
                    }
                    else
                    {
                        result.Append(char.ToLowerInvariant(character));
                    }
                }
                else
                {
                    result.Append(character);
                }
            }

            return result.ToString();
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.ForEachStringResourceEntry(InvertCaseLogic);
        }
    }
}
