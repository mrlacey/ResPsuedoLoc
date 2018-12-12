// <copyright file="ReverseCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class ReverseCommand : BaseCommand
    {
        public const int CommandId = 4127;

        private ReverseCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            commandService.AddCommand(menuItem);
        }

        public static ReverseCommand Instance
        {
            get;
            private set;
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new ReverseCommand(package, commandService);
        }

        public static string ReverseLogic(string input)
        {
            var surrounded = SurroundCommand.IsSurrounded(input);

            var stringToReverse = input;

            if (surrounded)
            {
                stringToReverse = SurroundCommand.RemoveSurrounds(input);
            }

            var result = stringToReverse.ReverseGraphemeClusters();

            if (surrounded)
            {
                result = SurroundCommand.SurroundLogic(result);
            }

            return result;
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.ForEachStringResourceEntry(ReverseLogic);
        }
    }
}
