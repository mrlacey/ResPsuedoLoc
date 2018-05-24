// <copyright file="SurroundCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class SurroundCommand : BaseCommand
    {
        public const int CommandId = 4128;

        private const string SurroundStart = "[! ";
        private const string SurroundEnd = " !]";

        private SurroundCommand(AsyncPackage package, OleMenuCommandService commandService)
            : base(package)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandId = new CommandID(PsuedoLocPackage.CommandSet, CommandId);
            var menuItem = new OleMenuCommand(this.Execute, menuCommandId);
            commandService.AddCommand(menuItem);
        }

        public static SurroundCommand Instance
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
            Instance = new SurroundCommand(package, commandService);
        }

        public static string SurroundLogic(string input)
        {
            if (IsSurrounded(input))
            {
                return RemoveSurrounds(input);
            }
            else
            {
                return $"{SurroundStart}{input}{SurroundEnd}";
            }
        }

        internal static string RemoveSurrounds(string input)
        {
            return input.TrimPrefix(SurroundStart).TrimSuffix(SurroundEnd);
        }

        internal static bool IsSurrounded(string input)
        {
            return input.StartsWith(SurroundStart) && input.EndsWith(SurroundEnd);
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.ForEachStringResourceEntry(SurroundLogic);
        }
    }
}
