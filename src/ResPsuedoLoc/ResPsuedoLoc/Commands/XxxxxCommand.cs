// <copyright file="XxxxxCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.Design;
using System.Text;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class XxxxxCommand : BaseCommand
    {
        public const int CommandId = 4131;

        private XxxxxCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            menuItem.BeforeQueryStatus += this.MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        public static XxxxxCommand Instance { get; private set; }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Verify the current thread is the UI thread - the call to AddCommand in DoubleCommand's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            var commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new XxxxxCommand(package, commandService);
        }

        public static string XxxxxLogic(string input)
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
                    if (char.IsUpper(letter))
                    {
                        result.Append('X');
                    }
                    else
                    {
                        result.Append('x');
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

            this.ForEachStringResourceEntry(XxxxxLogic);
        }
    }
}
