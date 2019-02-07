// <copyright file="SurroundCommand.cs" company="Matt Lacey Ltd.">
// Copyright (c) Matt Lacey Ltd. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using Microsoft.VisualStudio.Shell;
using Task = System.Threading.Tasks.Task;

namespace ResPsuedoLoc.Commands
{
    public sealed class SurroundCommand : ReversableCommand
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
            menuItem.BeforeQueryStatus += this.MenuItem_BeforeQueryStatus;
            commandService.AddCommand(menuItem);
        }

        private SurroundCommand()
            : base()
        {
            // For testing use only
        }

        public static SurroundCommand Instance { get; private set; }

        public static SurroundCommand CreateForTesting()
        {
            return new SurroundCommand();
        }

        public static async Task InitializeAsync(AsyncPackage package)
        {
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync();

            OleMenuCommandService commandService = await package.GetServiceAsync(typeof(IMenuCommandService)) as OleMenuCommandService;
            Instance = new SurroundCommand(package, commandService);
        }

        public static string SurroundLogic(string input, ToggleMode toggleMode)
        {
            if (toggleMode == ToggleMode.NotSet)
            {
                return input;
            }

            if (toggleMode == ToggleMode.Apply)
            {
                if (IsSurrounded(input))
                {
                    return input;
                }
                else
                {
                    return $"{SurroundStart}{input}{SurroundEnd}";
                }
            }
            else
            {
                if (IsSurrounded(input))
                {
                    return RemoveSurrounds(input);
                }
                else
                {
                    return input;
                }
            }
        }

        public string SurroundLogic(string input)
        {
            if (this.Mode == ToggleMode.NotSet)
            {
                this.Mode = this.GetToggleMode(input);
            }

            return SurroundLogic(input, this.Mode);
        }

        public override List<string> TestActingOnMultipleStrings(List<string> inputs)
        {
            var result = new List<string>();

            foreach (var input in inputs)
            {
                result.Add(this.SurroundLogic(input));
            }

            return result;
        }

        internal static string RemoveSurrounds(string input)
        {
            return input.TrimPrefix(SurroundStart).TrimSuffix(SurroundEnd);
        }

        internal static bool IsSurrounded(string input)
        {
            return input.StartsWith(SurroundStart) && input.EndsWith(SurroundEnd);
        }

        internal new ToggleMode GetToggleMode(string input)
        {
            return IsSurrounded(input) ? ToggleMode.Reverse : ToggleMode.Apply;
        }

        private void Execute(object sender, EventArgs e)
        {
            ThreadHelper.ThrowIfNotOnUIThread();

            this.Mode = ToggleMode.NotSet;

            this.ForEachStringResourceEntry(this.SurroundLogic);
        }
    }
}
